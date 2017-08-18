using System;
using TelegramBotAPIClient.Services;
using TelegramBotAPIClient.Configurations;
using System.Net.Http;

namespace TelegramBotAPIClient
{
    public class TelegramBotAPIClient : ITelegramBotAPIClient
    {
        private readonly IHttpService _httpService;

        public TelegramBotAPIClientConfiguration Configuration;

        public TelegramBotAPIClient(string authenticationToken)
        {
            string apiBaseUrl = $"https://api.telegram.org/bot{authenticationToken}";
            _httpService = new HttpService(apiBaseUrl);
            Configuration = new TelegramBotAPIClientConfiguration(authenticationToken);
        }

        public bool SendMessage(int chat_id, string text)
        {
            if (string.IsNullOrEmpty(text)) throw new Exception("");

            object response = _httpService.GetWebApi<object>($"/sendMessage?chat_id={chat_id}&text={text}");

            return true;
        }
        

    }
}
