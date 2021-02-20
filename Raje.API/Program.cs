using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Raje.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Obtem a variavel para definir com o ambiente
            //Isso pode ser configurado através das variaveis de ambiente do Windows
            //Ou através das configurações no startup
            var currentEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            //Lê os arquivos de configuração para definir antes da inicialização do projeto
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{currentEnv}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            //Configure logger
            //Criar um log de execução do projeto, para tratar erros fora do escopo
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {
                Log.Information("Starting Web host...");
                CreateHostBuilder(args).Build().Run();
                Log.Information("...Web host stopped");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Web Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
