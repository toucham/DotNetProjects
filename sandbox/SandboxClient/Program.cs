// See https://aka.ms/new-console-template for more information
using System;

namespace SandboxClient;

class Program
{
    static void Main(string[] args)
    {
        // get filename
        string requestFile = "";
        string timelineFile = "";
        if (args.Length > 0)
        {
            requestFile = args[0];
        }
        if (args.Length > 1)
        {
            timelineFile = args[1];
        }

        // parse the input file to get fake requests
        var client = new ClientBuilder()
            .AddRequestFile(requestFile)
            .AddTimelineFile(timelineFile)
            .Build();

        // start sending
        client.Start();
    }
}
