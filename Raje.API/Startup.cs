using Raje.BLL.Injections;
using Raje.BLL.Services.Admin;
using Raje.DAL.EF;
using Raje.DL.DB.Admin;
using Raje.DL.Services.BLL.Base;
using Raje.DL.Services.BLL.Identity;
using Raje.Infra.Const;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using Raje.BLL.Services.Identity.Jwt;
using AutoMapper;
using Raje.BLL.Mapper;

namespace Raje.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public const string CorsAllow = "AllowAny";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.Configure<FormOptions>(o =>  // currently all set to max, configure it to your needs!
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = 52428800; // 50mb
                o.MultipartBoundaryLengthLimit = int.MaxValue;
                o.MultipartHeadersCountLimit = int.MaxValue;
                o.MultipartHeadersLengthLimit = int.MaxValue;
            });

            ConfigAuthorizationService(services);
            ConfigDataBaseService(services);
            EFRepositoryDI.Config(services);
            BLLServicesDI.Config(services);
            ConfigureCorsService(services);

            services.AddControllers()
                .AddNewtonsoftJson(
                        options =>
                        {
                            options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            })
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            ConfigureSwaggerService(services);

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.Configure<SMTPOptions>(Configuration.GetSection("SMTPSettings"));
            services.Configure<ResetPasswordOptions>(Configuration.GetSection("ResetPasswordSettings"));
            services.Configure<TaxReceiptSettings>(Configuration.GetSection("TaxReceiptSettings"));

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
            });

            services.AddApplicationInsightsTelemetry(Configuration);

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ContentsProfile());
                mc.AddProfile(new LogProfile());
                mc.AddProfile(new UserProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app
            , IWebHostEnvironment env
            , ILoggerFactory loggerFactory
            , IApiVersionDescriptionProvider apiProviderDescription)
        {
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = 52428800; // 50Mb
                await next.Invoke();
            });

            app.UseAuthentication();
            app.UseCors(CorsAllow);
            app.UseRouting();
            app.UseAuthorization();
            ConfigureSwagger(app, apiProviderDescription);

            //Add our new middleware to the pipeline
            app.UseMiddleware<LoggingMiddlewareService>();

            app.UseResponseCompression();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static readonly LoggerFactory ConsoleLoggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() });

        #region [Authorization]
        public virtual void ConfigAuthorizationService(IServiceCollection services)
        {
            var jwtOptions = new JwtOptions();
            Configuration.GetSection("jwt").Bind(jwtOptions);
            services.AddSingleton(jwtOptions);
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "MultipleAuthentication";
                options.DefaultChallengeScheme = "MultipleAuthentication";
            })
            .AddPolicyScheme("MultipleAuthentication", "Authorization Bearer or APIKey", options =>
            {
                options.ForwardDefaultSelector = context =>
                {
                    return JwtBearerDefaults.AuthenticationScheme;
                };
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.JwtIssuer,
                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.JwtIssuer,

                    // The signing key must match!
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.JwtKey)),

                    // Validate the token expiry
                    ValidateLifetime = true,
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.UserToken, policy => policy.Requirements.Add(new UserAuthenticationRequirement()));
            });

            services.AddScoped<IAuthorizationHandler, UserAuthenticationHandler>();

            //Disponibilizar o usuário logado através de DI
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>
                (provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        }

        #endregion

        #region [Swagger]
        public virtual void ConfigureSwaggerService(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "RAJE API v1.0", Version = "v1.0" });

                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });
            });
        }

        public virtual void ConfigureSwagger(IApplicationBuilder app, IApiVersionDescriptionProvider apiProviderDescription)
        {
            app.UseSwagger()
               .UseSwaggerUI(options =>
               {
                   foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                   {
                       options.SwaggerEndpoint(
                           $"/swagger/{description.GroupName}/swagger.json",
                           description.GroupName.ToUpperInvariant());
                   }
                   options.RoutePrefix = "";
               });
        }

        #endregion

        #region [Cors]

        public virtual void ConfigureCorsService(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsAllow,
                builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }

        #endregion

        #region [DataBase]
        public virtual void ConfigDataBaseService(IServiceCollection services)
        {
            services.AddDbContext<DbContext, EntityContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
                options.UseLoggerFactory(ConsoleLoggerFactory);
            });

            services.AddDbContext<EntityContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
                options.UseLoggerFactory(ConsoleLoggerFactory);
            });
        }

        #endregion
    }

}
