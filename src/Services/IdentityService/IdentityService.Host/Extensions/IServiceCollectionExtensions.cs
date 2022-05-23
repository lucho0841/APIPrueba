using Hellang.Middleware.ProblemDetails;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace IdentityService.Host.Extensions
{
    public static class IServiceCollectionExtensions
    {

        public static IServiceCollection AddCustomerSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen( opts =>
            {
                opts.DescribeAllParametersInCamelCase();
                opts.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Capa de negocios", Version = "V1.0.0" });
                opts.TagActionsBy(opts => new[] { opts.GroupName });
                opts.DocInclusionPredicate((name, api) => true);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opts.IncludeXmlComments(xmlPath);
            });

            return services;
        }
        public static IServiceCollection AddCustomerMvc(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            return services;
        }

        public static IServiceCollection AddCustomerProblemDetail(this IServiceCollection services) =>
            services.AddProblemDetails(configure =>
            {
                configure.IncludeExceptionDetails = (ctx, ex) =>
                {
                    var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();
                    return env.IsDevelopment() || env.IsStaging();
                };
            });
        

        
    }
}
