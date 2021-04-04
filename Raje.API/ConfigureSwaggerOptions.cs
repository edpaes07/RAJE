using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace Raje.API
{
    /// <summary>
    /// Configures the Swagger generation options.
    /// </summary>
    /// <remarks>This allows API versioning to define a Swagger document per API version after the
    /// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "PR PROF ADS – RAJE",
                Version = description.ApiVersion.ToString(),
                Description = "API para uso do aplicativo Raje desenvolvido no trabalho de Tecnologia em Análise e Desenvolvimento de Sistemas.<br/><br/>"
                + "<b>Integrantes:</b><br/>"
                + "Nome: Aline de Oliveira Soares – TIA: 19004346<br/>"
                + "Nome: Edmilson Bispo Paes dos Santos – TIA: 19010291<br/>"
                + "Nome: Joni Welter Ramos – TIA: 19005636<br/>"
                + "Nome: Rita de Cassia Duarte Garcia – TIA: 19000448",
                Contact = new OpenApiContact() { Name = "Professor Fabio Silva Lopes", Url = new Uri("https://eadgrad.mackenzie.br/user/view.php?id=469&course=1") },
                License = new OpenApiLicense() { Name = "Prática profissional em análise e desenvolvimento de sistemas", Url = new Uri("https://eadgrad.mackenzie.br/course/view.php?id=3373") }
            };

            if (description.IsDeprecated)
            {
                info.Description += "Esta versão da API foi descontinuada.";
            }

            return info;
        }
    }
}