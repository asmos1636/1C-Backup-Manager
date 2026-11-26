using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneCBackupManager.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddWindowsService(options => options.ServiceName = "1C Backup Manager Worker");
builder.Services.AddHostedService<Worker>();
await builder.Build().RunAsync();
