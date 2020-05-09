using Elmah.Io.AspNetCore;
using Elmah.Io.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DevIO.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLogginConfiguration(this IServiceCollection services)
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
            return services;
        }

        public static IApplicationBuilder UseLogginConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            return app;
        }
    }
}
