using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;

namespace PlexureAPITest.Helpers
{
    public static class ApiHelper
    {
        private static RestClient _client;
        private static readonly object _locker = new object();

        private static RestClient Client
        {
            get
            {
                lock (_locker)
                {
                    if (_client == null)
                    {
                        _client = new RestClient(new RestClientOptions
                        {
                            BaseUrl = new Uri(GlobalSetup._baseUrl)
                        });
                    }
                    return _client;
                }
            }
        }

        public static void RestClientDispose()
        {
            _client.Dispose();
        }

        public static RestResponse PrepareRestResponseForPostRequest(string requestAddress, dynamic requestBody)
        {
            RestRequest request = PreparePostRequest(requestAddress, requestBody);
            return Client.ExecutePostAsync(request).Result;
        }

        public static RestResponse PrepareRestResponseForGetRequest(string requestAddress)
        {
            RestRequest request = PrepareGetRequest(requestAddress);
            return Client.Get(request);
        }

        public static dynamic DeserializeRestResponseToDynamicObject(RestResponse response)
        {
            return JsonConvert.DeserializeObject<dynamic>(response.Content);
        }

        public static void CheckIfStatusCodeIsAsExpected(RestResponse restResponse, HttpStatusCode expectedStatus)
        {
            Console.Write($"Check that response status is as expected: '{expectedStatus}' \n");
            Assert.That(restResponse.StatusCode.Equals(expectedStatus));
        }

        private static RestRequest PreparePostRequest(string requestAddress, object data)
        {
            RestRequest request = new RestRequest(requestAddress);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("token", GlobalSetup._token);
            request.AddJsonBody(data);
            return request;
        }

        private static RestRequest PrepareGetRequest(string requestAddress)
        {
            RestRequest request = new RestRequest(requestAddress);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("token", GlobalSetup._token);
            return request;
        }
    }
}