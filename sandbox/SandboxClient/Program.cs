// See https://aka.ms/new-console-template for more information
using System;

namespace SandboxClient;

class Program
{
    static void Main(string[] args)
    {
        // get filename
        string filename = "";
        if (args.Length > 0)
        {
            filename = args[0];
        }

        // parse the input file to get fake requests
        var client = new ClientBuilder().AddFileName(filename).Parse().Build();

        // start sending
        client.Start();
    }
}
