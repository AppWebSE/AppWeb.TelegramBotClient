using Newtonsoft.Json;
using System.Collections.Generic;
using TelegramBotAPIClient.Converters;

namespace TelegramBotAPIClient.Configurations
{
    public class TelegramClientConfiguration
    {
        public string AuthenticationToken { get; private set; }
        public static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter>
            {
                new UnixDateTimeConverter()
            }
        };

        public TelegramClientConfiguration(string authenticationToken)
        {
            AuthenticationToken = authenticationToken;
        }

    }
}
