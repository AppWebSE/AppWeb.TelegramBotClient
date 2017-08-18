using System;
using TelegramBotAPIClient.Service;

namespace TelegramBotAPIClient
{
    public class TelegramBotAPIClient : ITelegramBotAPIClient
    {
        IHttpService _httpService;

        public TelegramBotAPIClient(IHttpService httpService)
        {
            httpService = _httpService;
        }

        // todo: implementations

    }
}
