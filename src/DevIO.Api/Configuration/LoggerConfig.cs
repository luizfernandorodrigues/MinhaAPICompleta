using DevIO.Api.Extensions;
using Elmah.Io.AspNetCore;
using Elmah.Io.AspNetCore.HealthChecks;
using Elmah.Io.Extensions.Logging;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DevIO.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLogginConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = "49a7dab3fa8140c79fa1eecc15edc8c5";
                o.LogId = new Guid("7383aa4a-d577-4d6d-af28-a0b7b0eb47e6");
            });



            // configuração para logs que nao são de erros de requisições e sim implementados manualmente (logs que não pertence ao pipe line ** provider)
            // necessario instalar o pacote: Install-Package Elmah.Io.Extensions.Logging
            //services.AddLogging(builder =>
            //{
            //    builder.AddElmahIo(o =>
            //    {
            //        o.ApiKey = "49a7dab3fa8140c79fa1eecc15edc8c5";
            //        o.LogId = new Guid("7383aa4a-d577-4d6d-af28-a0b7b0eb47e6");
            //    });
            //    builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning); // definição de filtro de logs
            //});

            // configura o monitoramento da api, neste caso o banco de dados
            // necessario instalar o pacote : AspNetCore.HealthChecks.Sqlserver
            services.AddHealthChecks()
                .AddElmahIoPublisher("49a7dab3fa8140c79fa1eecc15edc8c5", new Guid("7383aa4a-d577-4d6d-af28-a0b7b0eb47e6"))
                .AddCheck("Produtos", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")))
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

            services.AddHealthChecksUI();// necessario o pacote : aspnetcore.healthchecks.ui
            return services;
        }

        public static IApplicationBuilder UseLogginConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            app.UseHealthChecks("/api/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.UseHealthChecksUI(options =>
            {
                options.UIPath = "/api/hc-ui";
            });

            return app;
        }
    }
}
