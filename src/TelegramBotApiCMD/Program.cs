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
        private const string ExitCommand                = "exit";
        private const string GetUpdatesCommand          = "getupdates";
        private const string HelpCommand                = "help";
        private const string DeleteWebhookCommand       = "deletewebhook";
        private const string SetWebhookCommand          = "setwebhook";
        private const string SetWebhookCommandFormat    = SetWebhookCommand + " <webhookUrl>";
        private const string SetTokenCommand            = "settoken";
        private const string SetTokenCommandFormat      = SetTokenCommand + " <token>";

        public Program()
        {
        }

        static void Main(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", true, true);
            // .AddJsonFile($"appsettings.{environmentName}.json", true, true);
            var Configuration = builder.Build();


            ITelegramClient telegramClient = new TelegramClient();
            
            Console.WriteLine("Telegram Bot console application started");
            
            var authenticationToken = Configuration["Settings:authenticationToken"];
            if (!string.IsNullOrEmpty(authenticationToken))
            {
                SetToken(telegramClient, authenticationToken);
            }

            PrintHelp();

            bool runLoop = true;

            while (runLoop) 
            {
                string line = Console.ReadLine(); 

                if (line.ToLower().Contains(ExitCommand))
                {
                    runLoop = false;
                }
                else if (line.ToLower().Contains(HelpCommand))
                {
                    PrintHelp();
                }
                else if (line.ToLower().Contains(GetUpdatesCommand))
                {
                    GetUpdates(telegramClient);
                }
                else if (line.ToLower().Contains(SetTokenCommand))
                {
                    var commands = line.Split(' ');
                    string token = commands.Length > 1 ? commands[1] : null;

                    SetToken(telegramClient, token);
                }
                else if (line.ToLower().Contains(DeleteWebhookCommand))
                {
                    DeleteWebhook(telegramClient);
                }
                else if (line.ToLower().Contains(SetWebhookCommand))
                {
                    var commands = line.Split(' ');
                    string webhookUrl = commands.Length > 1 ? commands[1] : null;

                    SetWebhook(telegramClient, webhookUrl);
                }
            }

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine($" {GetUpdatesCommand}");
            Console.WriteLine($" {SetTokenCommandFormat}");
            Console.WriteLine($" {DeleteWebhookCommand}");
            Console.WriteLine($" {SetWebhookCommandFormat}");
            Console.WriteLine($" {ExitCommand}");
            Console.WriteLine($" {HelpCommand}");
        }

        private static void SetToken(ITelegramClient telegramClient, string authenticationToken)
        {
            try
            {
                telegramClient.SetAuthenticationToken(authenticationToken);
            }
            catch(Exception e)
            {
                Console.WriteLine("Authentication token could not be set, make sure it's correct");
                return;
            }

            Console.WriteLine("Authentication token is now set");
        }

        private static void DeleteWebhook(ITelegramClient telegramClient)
        {
            try
            {
                telegramClient.DeleteWebhook();
            }
            catch(Exception e)
            {
                Console.WriteLine("Could not delete webhook");
                return;
            }
            Console.WriteLine("Webhook deleted");
        }

        private static void SetWebhook(ITelegramClient telegramClient, string webhookUrl)
        {
            try
            {
                UpdateType[] allowedUpdates = { UpdateType.MessageUpdate };

                telegramClient.SetWebhook(webhookUrl, 10, allowedUpdates);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not set webhook");
                return;
            }
            Console.WriteLine("Webhook set");
        }

        private static void GetUpdates(ITelegramClient telegramClient)
        {
            UpdateType[] updateTypes = {
                UpdateType.All
            };
            Update[] updates = null;

            try
            {
                 updates= telegramClient.GetUpdates(0, 100, 0, updateTypes);
            }
            catch(Exception e)
            {
                Console.WriteLine("Could not get updates, make sure you don't have an active webHook since that will prevent from getting updates manually");
                return;
            }

            Console.WriteLine($"{updates.Length} update(s) found");

            foreach(var update in updates)
            {
                Console.WriteLine("---");
                Console.WriteLine($"Update: {update.Id}");
                Console.WriteLine($"ChatId: {update.Message.Chat.Id}");
                Console.WriteLine($"Message: {update.Message.Text}");
                Console.WriteLine($"Timestamp: {update.Message.Date.ToString()}");
            }
        }

    }
}