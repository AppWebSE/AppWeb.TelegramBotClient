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
