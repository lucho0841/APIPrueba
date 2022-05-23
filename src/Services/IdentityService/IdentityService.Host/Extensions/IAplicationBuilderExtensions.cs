using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Host.Extensions
{
    public static class IAplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomerSwagger(this IApplicationBuilder app)
        => app
            .UseSwagger()
            .UseSwaggerUI(c => {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "Capa de negocios");
            });
        
    }
}
