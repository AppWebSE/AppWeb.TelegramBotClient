using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotAPIClient.Service
{
    public interface IHttpService
    {
        T GetWebApi<T>(string apiUrl);
        T PostWebApi<T>(object data, string apiUrl);
    }
}
