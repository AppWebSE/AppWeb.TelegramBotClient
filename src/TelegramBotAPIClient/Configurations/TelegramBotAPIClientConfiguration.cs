using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotAPIClient.Configurations
{
    public class TelegramBotAPIClientConfiguration
    {
        public string AuthenticationToken { get; private set; }

        public TelegramBotAPIClientConfiguration(string authenticationToken)
        {
            AuthenticationToken = authenticationToken;
        }

    }
}
