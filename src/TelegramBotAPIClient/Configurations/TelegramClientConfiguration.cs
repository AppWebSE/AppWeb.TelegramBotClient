namespace TelegramBotAPIClient.Configurations
{
    public class TelegramClientConfiguration
    {
        public string AuthenticationToken { get; private set; }
        
        public TelegramClientConfiguration(string authenticationToken)
        {
            AuthenticationToken = authenticationToken;
        }

    }
}
