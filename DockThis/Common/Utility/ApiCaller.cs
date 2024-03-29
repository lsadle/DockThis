﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Common.Api;
using Common.Utility.Interfaces;
using Newtonsoft.Json;

namespace Common.Utility
{
    public class ApiCaller : IApiCaller
    {
        private readonly string _dbApiBase;
        private readonly HttpClient _client;
        public ApiCaller(string dbApiBase)
        {
            _dbApiBase = dbApiBase;
            _client = new HttpClient();
        }

        private async Task<HttpResponseMessage> MakeRequest(HttpMethod method, string endpoint, object content = null)
        {
            var fullEndpoint = $"{_dbApiBase}{endpoint}";
            var request = new HttpRequestMessage(method, fullEndpoint);

            if (content != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(content));
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            }

            var response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Api call failed\r\nResponse Code: {response.StatusCode}\r\nResponse Reason: {response.ReasonPhrase}\r\nMethod: {method}\r\nEndpoint: {endpoint}\r\nContent: {JsonConvert.SerializeObject(content)}");
            }

            return response;
        }


        public async Task CallApi(HttpMethod method, string endpoint, object content)
        {
            await MakeRequest(method, endpoint, content);
        }

        public async Task<T> CallApi<T>(HttpMethod method, string endpoint)
        {
            var response = await MakeRequest(method, endpoint);

            if (response.Content != null)
            {
                var contentBytes = await response.Content.ReadAsByteArrayAsync();
                var contentString = Encoding.ASCII.GetString(contentBytes);
                var content = JsonConvert.DeserializeObject<T>(contentString);
                return content;
            }

            return default(T);
        }
    }
}
