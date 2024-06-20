using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AppWeb.TelegramBotClient.Enums;

namespace AppWeb.TelegramBotClient.Webhook
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(x => x.EnableEndpointRouting = false );

            // Setup telegram client
            TelegramClient telegramClient = new TelegramClient();

            var authenticationToken = Configuration["Settings:authenticationToken"];
            telegramClient.SetAuthenticationToken(authenticationToken);

            // Set up webhook
            string webhookUrl = Configuration["Settings:webhookUrl"];
            int maxConnections = int.Parse(Configuration["Settings:maxConnections"]);
            UpdateType[] allowedUpdates = { UpdateType.MessageUpdate };

            telegramClient.SetWebhook(webhookUrl, maxConnections, allowedUpdates);

            services.AddSingleton<ITelegramClient>(client => telegramClient);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
