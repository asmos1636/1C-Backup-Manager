using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddWindowsService(options => options.ServiceName = "1C Backup Manager Worker");
builder.Services.AddHostedService<Worker>();
await builder.Build().RunAsync();
