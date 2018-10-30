using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AppWeb.TelegramBotClient;
using AppWeb.TelegramBotClient.Enums;

namespace AppWeb.TelegramBotClient.Webhook
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // Setup telegram client
            TelegramClient telegramClient = new TelegramClient();

            var authenticationToken = Configuration["Settings:authenticationToken"];
            telegramClient.SetAuthenticationToken(authenticationToken);
            
            // Set up webhook
            string webhookUrl = Configuration["Settings:webhookUrl"];
            int maxConnections = int.Parse(Configuration["Settings:maxConnections"]);
            UpdateType[] allowedUpdates = { UpdateType.MessageUpdate };

            telegramClient.SetWebhook(webhookUrl, maxConnections, allowedUpdates);

            services.AddScoped<ITelegramClient>(client => telegramClient);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
