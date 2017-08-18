using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotAPIClient
{
    public interface ITelegramBotAPIClient
    {
        bool SendMessage(int chat_id, string text);
    }
}
