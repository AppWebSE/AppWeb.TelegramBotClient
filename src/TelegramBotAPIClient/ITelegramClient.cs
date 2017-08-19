using TelegramBotAPIClient.Enums;
using TelegramBotAPIClient.Models;

namespace TelegramBotAPIClient
{
    public interface ITelegramClient
    {
        Message SendMessage(int chat_id, string text);
        Update[] GetUpdates(int? offset, int? limit, int? timeout, UpdateType[] allowedUpdates = null);
    }
}
