using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using TelegramBotAPIClient;
using TelegramBotAPIClient.Enums;
using TelegramBotAPIClient.Models;

namespace TelegramBotApiClient.ExampleUsage
{
    public class Program
    {

        public Program()
        {
        }

        static void Main(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true);
            var Configuration = builder.Build();
           
            var accessToken = Configuration["Settings:accessToken"];

            ITelegramClient _apiClient = new TelegramClient(accessToken);

            Console.WriteLine("Started");

            UpdateType[] updateTypes = {
                UpdateType.All
            };

            Update[] updates = _apiClient.GetUpdates(0, 100, 0, updateTypes);


            Console.WriteLine("Messages read");
        }
    }
}