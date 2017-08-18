using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotAPIClient.Services
{
    public interface IHttpService
    {
        void GetWebApi(string apiUrl);
        void PostWebApi(object data, string apiUrl);
        T GetWebApi<T>(string apiUrl);
        T PostWebApi<T>(object data, string apiUrl);
    }
}
