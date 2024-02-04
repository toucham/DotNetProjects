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
            builder.AddConsole();
        });
        var logger = factory.CreateLogger<Program>();

        // instantiate config
        var configRoot = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var config = configRoot.GetRequiredSection("Setting").Get<Setting>();
        if (config == null)
        {
            throw new Exception("Setting property is not found in appsettings.json");
        }

        // setup the client to start simulating events
        var client = new ClientBuilder().AddSetting(config).Build();
        if (client == null) throw new Exception("client is null");

        // start sending
        await client.Start();
    }
}
