using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using NestStream.Client;

namespace NestStream
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            string streamingUri = config["NestApi:StreamingUri"];
            string accessToken = config["NestApi:AccessToken"];

            NestStreamClient client = new NestStreamClient(streamingUri, accessToken);
            client.ReceivedNestStreamEvent += (s, e) =>
            { 
                string logTime = e.Timestamp.ToShortTimeString();
                switch (e.Type)
                {
                    case "put":
                        foreach (var t in ThermostatData.Parse(e.Data)) { Console.WriteLine($"{logTime} [{e.Type}] {t.ToString()}"); }
                        break;
                    default:
                        //Console.WriteLine($"{logTime} [{e.Type}] {e.Data.TrimEnd()}");
                        break;
                }
            };
            client.Connect();

            Console.ReadKey();
        }
    }
}