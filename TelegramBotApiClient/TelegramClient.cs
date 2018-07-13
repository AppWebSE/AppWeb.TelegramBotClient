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
        private string AuthenticationToken { get; set; }

        private string BaseUrl => $"https://api.telegram.org/bot{AuthenticationToken}";

        public TelegramClient()
        {
            _httpService = new HttpService(TelegramClientConfiguration.SerializerSettings);
        }
        
        public void SetAuthenticationToken(string authenticationToken)
        {
            if (string.IsNullOrEmpty(authenticationToken) || !Regex.IsMatch(authenticationToken, @"^\d*:[\w\d-_]{35}$"))
            {
                throw new ArgumentException("Invalid token format", nameof(authenticationToken));
            }

            AuthenticationToken = authenticationToken;
        }

        public string GetAuthenticationToken()
        {
            return AuthenticationToken;
        }

        public Message SendMessage(long chat_id, string text)
        {
            if (string.IsNullOrEmpty(text)) throw new Exception("");

            var endpoint = $"{BaseUrl}/sendMessage?chat_id={chat_id}&text={text}";

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
            
            return _httpService.PostWebApi<Update[]>(parameters, $"{BaseUrl}/getUpdates");
            
        }

        public void DeleteWebhook()
        {
            _httpService.GetWebApi($"{BaseUrl}/deleteWebhook");
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

            return _httpService.PostWebApi<bool>(parameters, $"{BaseUrl}/setWebhook");
        }
    }
}
