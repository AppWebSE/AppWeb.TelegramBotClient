using TelegramBotAPIClient.Models;

namespace TelegramBotAPIClient
{
    public interface ITelegramBotAPIClient
    {
        Message SendMessage(int chat_id, string text);
    }
}
