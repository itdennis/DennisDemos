using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace anc.middleware.test
{
    public class LogsMiddleware
    {
        private readonly RequestDelegate _next;

        public LogsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context) 
        {
            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            if (endpoint == null)
            {
                await _next(context);
                return;
            }

            using (var scope = context.RequestServices.CreateScope())
            {
                var _logger = scope.ServiceProvider.GetService<ILogger<LogsMiddleware>>();
                var attributes = endpoint.Metadata.OfType<NoLogsAttriteFilter>();
                if (attributes.Count() == 0)
                {
                    _logger.LogInformation($" url:{context.Request.Path}, 访问时间:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                }

                foreach (var attribute in attributes)
                {
                    _logger.LogInformation(attribute.Message);
                }

                await _next(context);
            }
        }
    }
}
