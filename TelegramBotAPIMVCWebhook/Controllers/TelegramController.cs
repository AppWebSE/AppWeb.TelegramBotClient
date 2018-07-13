using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TelegramBotAPIClient;
using Newtonsoft.Json;
using TelegramBotAPIClient.Models;
using TelegramBotAPIClient.Configurations;

namespace TelegramBotApiClient.ExampleMVCWebhookApplication.Controllers
{
    [Route("api/[controller]")]
    public class TelegramController : Controller
    {
        ITelegramClient _telegramClient;
        public TelegramController(ITelegramClient telegramClient) {
            _telegramClient = telegramClient;
        }

        // GET api/telegram/update/{token}
        [HttpPost("update/{token}")]
        public void Update([FromRoute]string token, [FromBody]Update update)
        {
            if (token != _telegramClient.GetAuthenticationToken())
                return;

            if(update != null && update.Message != null)
                _telegramClient.SendMessage(update.Message.Chat.Id, $"I received your message: \"{update.Message.Text}\"");
            
            return;
        }
    }
}
