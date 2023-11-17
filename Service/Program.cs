using Service;
using Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Network;

var builder = Host.CreateApplicationBuilder(args);

var localConfigPath =
    $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.config/evl-daemon/config.json";

builder.Configuration.AddJsonFile(localConfigPath, optional: true);

builder.Services.AddOptions<ConnectionOptions>()
    .Bind(builder.Configuration.GetSection("Connection"));

builder.Services.AddOptions<LoggingOptions>()
    .Bind(builder.Configuration.GetSection("Logging"));

builder.Services.AddHostedService<ConsoleHost>();

builder.Services.AddHostedService<EvlDaemonService>();

builder.Services.AddSingleton<IEvlClient, NetworkEvlClient>();

builder.Services.AddLogging();

using var host = builder.Build();

host.Run();

