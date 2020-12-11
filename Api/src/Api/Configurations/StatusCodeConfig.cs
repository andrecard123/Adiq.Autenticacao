using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Adiq.Common.Models;

namespace Adiq.Presentation.Presentation.Api.Configurations
{
    internal static class StatusCodeConfig
    {
        public static IApplicationBuilder UseCustomStatusCode(this IApplicationBuilder app)
        {
            app
                .UseStatusCodePages(generalConfig => generalConfig.Map(
                    new PathString("/api"),
                        apiConfig => apiConfig.UseStatusCodePages(async (StatusCodeContext context) =>
                        {
                            await context.Next(context.HttpContext);

                            if (ConvertHttpStatusCodeToError(context.HttpContext.Response.StatusCode) is Error error)
                            {
                                var result = JsonSerialize(new[] { error });
                                context.HttpContext.Response.ContentType = "application/json";
                                await context.HttpContext.Response.WriteAsync(result);
                            }
                        })
                    )
                );

            return app;
        }

        public static Error ConvertHttpStatusCodeToError(int statusCode)
        {
            switch (statusCode)
            {
                case StatusCodes.Status404NotFound:
                    return new Error { Code = "NOT_FOUND", Message = "Recurso solicitado não foi encontrado." };

                case StatusCodes.Status500InternalServerError:
                    return new Error { Code = "ERROR", Message = "Ocorreu um erro interno ao realizar a operação." };

                default:
                    return default;
            }
        }

        private static string JsonSerialize(object @object) =>
           Newtonsoft.Json.JsonConvert.SerializeObject(
               @object
               , new Newtonsoft.Json.JsonSerializerSettings
               {
                   ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                   DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Unspecified
               }
           );
    }
}
