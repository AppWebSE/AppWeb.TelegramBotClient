using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using AppWeb.TelegramBotClient.Configurations;
using AppWeb.TelegramBotClient.Exceptions;
using AppWeb.TelegramBotClient.Models;

namespace AppWeb.TelegramBotClient.Services
{
    public class HttpService : IHttpService, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerSettings _serializerSettings;

        public HttpService(JsonSerializerSettings serializerSettings, HttpClient httpClient = null)
        {
            _httpClient = httpClient ?? new HttpClient();
            _httpClient.Timeout = new TimeSpan(0, 0, 10); //10 second timeout, make this an configuration
            _serializerSettings = serializerSettings;
        }

        public void GetWebApi(string apiUrl)
        {
            try
            {
                var response = _httpClient.GetAsync(apiUrl).Result;
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                throw new HttpException("HttpGet failed in HttpService", e);
            }
        }

        public T GetWebApi<T>(string apiUrl)
        {
            try
            {
                var response = _httpClient.GetAsync(apiUrl).Result;
                response.EnsureSuccessStatusCode();
                
                var responseString = response.Content.ReadAsStringAsync().Result;

                var responseObject = JsonConvert.DeserializeObject<ApiResponse<T>>(responseString, _serializerSettings);
                return responseObject.ResultObject;
            }
            catch (Exception e)
            {
                throw new HttpException("HttpGet failed in HttpService", e);
            }
        }

        public void PostWebApi(object data, string apiUrl)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);

                using (var content = new StringContent(JsonConvert.SerializeObject(data, _serializerSettings), Encoding.UTF8, "application/json"))
                {
                    request.Content = content;

                    var response = _httpClient.SendAsync(request).Result;
                    response.EnsureSuccessStatusCode();

                }
            }
            catch (Exception e)
            {
                throw new HttpException("HttpPost failed in HttpService", e);
            }
        }

        public T PostWebApi<T>(object data, string apiUrl)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);

                using (var content = new StringContent(JsonConvert.SerializeObject(data, _serializerSettings), Encoding.UTF8, "application/json"))
                {
                    request.Content = content;

                    var response = _httpClient.SendAsync(request).Result;
                    response.EnsureSuccessStatusCode();
                    
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    var responseObject = JsonConvert.DeserializeObject<ApiResponse<T>>(responseString, _serializerSettings);

                    return responseObject.ResultObject;

                }
            }
            catch (Exception e)
            {
                throw new HttpException("HttpPost failed in HttpService", e);
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
