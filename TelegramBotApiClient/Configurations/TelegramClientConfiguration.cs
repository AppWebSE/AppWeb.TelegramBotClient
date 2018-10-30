using Newtonsoft.Json;
using System.Collections.Generic;
using AppWeb.TelegramBotClient.Converters;

namespace AppWeb.TelegramBotClient.Configurations
{
    public class TelegramClientConfiguration
    {
        public static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter>
            {
                new UnixDateTimeConverter()
            }
        };
    }
}
