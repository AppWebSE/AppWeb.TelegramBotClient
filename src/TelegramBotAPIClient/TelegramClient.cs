using System;
using TelegramBotAPIClient.Services;
using TelegramBotAPIClient.Configurations;
using TelegramBotAPIClient.Models;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using TelegramBotAPIClient.Enums;
using System.Linq;

namespace TelegramBotAPIClient
{
    public class TelegramClient : ITelegramClient
    {
        private readonly IHttpService _httpService;

        public TelegramClientConfiguration Configuration;
        public TelegramClientConfiguration GetConfiguration()
        {
            return Configuration;
        }

        public TelegramClient(string authenticationToken)
        {
            if (!Regex.IsMatch(authenticationToken, @"^\d*:[\w\d-_]{35}$"))
                throw new ArgumentException("Invalid token format", nameof(authenticationToken));

            string apiBaseUrl = $"https://api.telegram.org/bot{authenticationToken}";
            Configuration = new TelegramClientConfiguration(authenticationToken);
            _httpService = new HttpService(apiBaseUrl, TelegramClientConfiguration.SerializerSettings);
        }
        
        public Message SendMessage(long chat_id, string text)
        {
            if (string.IsNullOrEmpty(text)) throw new Exception("");

            var endpoint = $"/sendMessage?chat_id={chat_id}&text={text}";

            return _httpService.GetWebApi<Message>(endpoint);
        }

        public Update[] GetUpdates(int? offset, int? limit, int? timeout, UpdateType[] allowedUpdates = null)
        {
            var parameters = new Dictionary<string, object> {
                { "offset", offset.HasValue ? offset.Value : 0 },
                { "limit", limit.HasValue ? limit.Value : 100 },
                { "timeout", 0 }
            };
            
            if (allowedUpdates != null && !allowedUpdates.Contains(UpdateType.All))
                parameters.Add("allowed_updates", allowedUpdates);
            
            return _httpService.PostWebApi<Update[]>(parameters, "/getUpdates");
            
        }

        public bool SetWebhook(string url, int maxConnections, UpdateType[] allowedUpdates = null)
        {
            var parameters = new Dictionary<string, object>
            {
                {"url", url},
                {"max_connections", maxConnections}
            };

            if (allowedUpdates != null && !allowedUpdates.Contains(UpdateType.All))
                parameters.Add("allowed_updates", allowedUpdates);

            return _httpService.PostWebApi<bool>(parameters, "/setWebhook");
        }
    }
}
