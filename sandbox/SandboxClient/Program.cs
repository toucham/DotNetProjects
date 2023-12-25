// See https://aka.ms/new-console-template for more information
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SandboxClient.Model;

namespace SandboxClient;

class Program
{
    static void Main(string[] args)
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
        var config = configRoot.GetRequiredSection("Configuration").Get<Config>();
        if (config == null)
        {
            throw new Exception("config file not found");
        }

        // parse the input file to get fake requests
        var client = new ClientBuilder().AddConfig(config).Build();

        // start sending
        client.Start();
    }
}
