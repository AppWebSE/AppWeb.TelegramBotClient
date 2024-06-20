using Microsoft.AspNetCore.Mvc;
using AppWeb.TelegramBotClient.Models;

namespace AppWeb.TelegramBotClient.Webhook.Controllers
{
    [Route("api/[controller]")]
    public class TelegramController : Controller
    {
        ITelegramClient _telegramClient;
        public TelegramController(ITelegramClient telegramClient) {
           _telegramClient = telegramClient;
        }
        
        // POST api/telegram/update/{token}
        [HttpPost("update/{token}")]
        public void Update([FromRoute]string token, [FromBody]Update update)
        {
            if (token != _telegramClient.GetAuthenticationToken())
                return;

            if (update != null && update.Message != null)
                _telegramClient.SendMessage(update.Message.Chat.Id, $"I received your message: \"{update.Message.Text}\"");

            return;
        }
    }
}
