using Newtonsoft.Json;
using TelegramBotAPIClient.Configurations;
using TelegramBotAPIClient.Enums;
using TelegramBotAPIClient.Models;

namespace TelegramBotAPIClient
{
    public interface ITelegramClient
    {
        TelegramClientConfiguration GetConfiguration();
        Message SendMessage(long chat_id, string text);
        Update[] GetUpdates(int? offset, int? limit, int? timeout, UpdateType[] allowedUpdates = null);
        bool SetWebhook(string url, int maxConnections, UpdateType[] allowedUpdates = null);
    }
}
