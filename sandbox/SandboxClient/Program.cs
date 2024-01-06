// See https://aka.ms/new-console-template for more information
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SandboxClient.Model;

namespace SandboxClient;

class Program
{
    static async Task Main(string[] args)
    {
        // instantiate logger
        using var factory = LoggerFactory.Create(static builder =>
        {
            builder
                .AddFilter("Microsft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("SandboxClient.Program", LogLevel.Information)
                .AddFilter("SandboxClient.Program", LogLevel.Debug)
                .AddFilter("SandboxClient.Program", LogLevel.Error)
                .AddConsole();
        });
        var logger = factory.CreateLogger<Program>();

        // instantiate config
        var configRoot = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var config = configRoot.GetRequiredSection("Setting").Get<Setting>();
        if (config == null)
        {
            throw new Exception("Setting not found");
        }

        // setup the client to start simulating events
        var client = new ClientBuilder().AddSetting(config).Build();

        // start sending
        await client.Start();
    }
}
