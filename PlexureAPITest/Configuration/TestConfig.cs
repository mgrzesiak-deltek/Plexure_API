using Newtonsoft.Json.Linq;
using PlexureAPITest.Helpers;
using PlexureAPITest.Model;
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
        public static string ReportNameFolder => _config[nameof(ReportNameFolder)].Value<string>();
        public static string Username => _config[nameof(Username)].Value<string>();
        public static string Password => _config[nameof(Password)].Value<string>();
        public static string AssemblyPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
        public static string GetAuthenticationToken()
        {
            var token = "37cb9e58-99db-423c-9da5-42d5627614c5";
            RestClient restClient = new RestClient();
            RestRequest authenticationRequest = new RestRequest($"{BaseUrl}/api{PostLoginRequest.Address}");
            authenticationRequest.AddParameter("username", Username);
            authenticationRequest.AddParameter("password", Password);
            RestResponse response = restClient.ExecutePostAsync(authenticationRequest).Result;
            //token = ApiHelper.DeserializeRestResponseToDynamicObject(response).ToObject<dynamic>().token;
            restClient.Dispose();
            return token;
        }
    }
}