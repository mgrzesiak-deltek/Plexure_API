using Newtonsoft.Json.Linq;
using PlexureAPITest.Helpers;
using PlexureAPITest.Requests;
using RestSharp;
using System;
using System.IO;
using System.Reflection;

namespace PlexureAPITest
{
    public static class TestConfig
    {
        private static readonly JObject _config;

        static TestConfig()
        {
            string tmp = string.Empty;
            string configFileName = @"\TestConfig.json";

            try
            {
                tmp = File.ReadAllText(AssemblyPath + configFileName);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            _config = JObject.Parse(tmp);
        }

        public static string BaseUrl => _config[nameof(BaseUrl)].Value<string>();
        public static string DefaultToken => _config[nameof(DefaultToken)].Value<string>();
        public static string ReportNameFolder => _config[nameof(ReportNameFolder)].Value<string>();
        public static string Username => _config[nameof(Username)].Value<string>();
        public static string Password => _config[nameof(Password)].Value<string>();
        public static string AssemblyPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
        public static string GetAuthenticationToken()
        {
            // Token should change after the login call. Also it should have some expiration date
            var token = DefaultToken;
            
            dynamic requestBody = new
            {
                Username = TestConfig.Username,
                Password = TestConfig.Password
            };
            
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{BaseUrl}/api{PostLoginRequest.Address}");
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody((object)requestBody);
            RestResponse response = client.ExecutePostAsync(request).Result;
            token = ApiHelper.DeserializeRestResponseToDynamicObject(response).ToObject<dynamic>().AccessToken;
            client.Dispose();
            return token;
        }
    }
}