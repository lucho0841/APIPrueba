
//SERVICIOS
using IdentityService.Host;

var builder = WebApplication.CreateBuilder(args);
HostExtensions.ConfigurationService(builder.Services);

//HOST
var app = builder.Build();
HostExtensions.Configure(app, host =>
{
    return host.UseDefaultFiles().UseStaticFiles();
});

//INICIO DEL PROYECTO
app.Run();