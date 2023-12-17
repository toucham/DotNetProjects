// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using SandboxClient.Model;

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
        var client = new ClientBuilder().AddFileName(filename).Build();

        // start sending
        client.Start();
    }

    static void ParseFile(string filename)
    {
        Console.WriteLine($"Reading file of name: {filename}...");
    }
}
