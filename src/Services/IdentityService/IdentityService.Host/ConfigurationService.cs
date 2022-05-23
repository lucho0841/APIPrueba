using Hellang.Middleware.ProblemDetails;
using IdentityService.Host.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Host
{
    public static class HostExtensions
    {
        public static IServiceCollection ConfigurationService(IServiceCollection services)
        {
            return
                services
                    .AddCustomerMvc()
                    .AddCustomerSwagger()
                    .AddCustomerProblemDetail();
        }

        public static IApplicationBuilder Configure(IApplicationBuilder app, Func<IApplicationBuilder, IApplicationBuilder> configurHost)
        {

            return configurHost(app)
                .UseProblemDetails()
                .UseHttpsRedirection()
                .UseAuthorization()
                .UseRouting()
                .UseCustomerSwagger()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=home}/{action=Index}/{id?}");
                });
        }
    }
}

