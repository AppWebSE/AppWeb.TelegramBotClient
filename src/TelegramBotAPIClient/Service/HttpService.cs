using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TelegramBotAPIClient.Exceptions;

namespace TelegramBotAPIClient.Service
{
    public class HttpService : IHttpService, IDisposable
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl.TrimEnd('/') + "/";
        }
        
        public T GetWebApi<T>(string apiUrl)
        {
            try
            {
                var response = _httpClient.GetAsync(_baseUrl + apiUrl).Result;
                response.EnsureSuccessStatusCode();
                
                var responseString = response.Content.ReadAsStringAsync().Result;
                
                return JsonConvert.DeserializeObject<T>(responseString);
            }
            catch (Exception e)
            {
                throw new HttpException("HttpGet failed in HttpService", e);
            }
        }
        
        public T PostWebApi<T>(object data, string apiUrl)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, _baseUrl + apiUrl);

                using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
                {
                    request.Content = content;

                    var response = _httpClient.SendAsync(request).Result;
                    response.EnsureSuccessStatusCode();
                    
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    
                    return JsonConvert.DeserializeObject<T>(responseString);

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
