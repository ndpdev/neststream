using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NestStream.Client;
using NestStream.Type;

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
                    case "keep-alive":
                        break;
                    case "put":
                        Console.Write(new StreamEvent(e.Data));
                        break;
                    default:
                        Console.WriteLine($"{logTime} [{e.Type}] {e.Data.TrimEnd()}");
                        break;
                }
            };

            client.Connect().Wait();
        }
    }
}