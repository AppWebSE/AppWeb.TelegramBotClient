using TelegramBotAPIClient.Enums;
using TelegramBotAPIClient.Models;

namespace TelegramBotAPIClient
{
    public interface ITelegramClient
    {
        Message SendMessage(int chat_id, string text);
        Update[] GetMessages(int? offset, int? limit, int? timeout, UpdateType[] allowedUpdates = null);
    }
}
