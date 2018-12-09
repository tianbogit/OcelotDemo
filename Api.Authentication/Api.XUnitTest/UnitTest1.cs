using System;
using Xunit;
using System.Diagnostics;
using RestSharp;

namespace Api.XUnitTest
{
    public class UnitTest1
    {
        /// <summary>
        /// 访问Url
        /// </summary>
        static string _url = "https://localhost:44366";

        [Fact]
        public void Test1()
        {
            dynamic token = null;

            //var stopwatch = new Stopwatch();
            //stopwatch.Start();


            token = AdminLogin();

            token = SystemLogin();

            //token = NullLogin();

            DemoAAPI(token);

            DemoBAPI(token);

            //stopwatch.Stop();
            //TimeSpan timespan = stopwatch.Elapsed;
            //Console.WriteLine($"间隔时间：{timespan.TotalSeconds}");

            tokenString = "Bearer " + Convert.ToString(token?.access_token);

        }

        static string tokenString = "";
        public dynamic NullLogin()
        {
            var loginClient = new RestClient(_url);
            var loginRequest = new RestRequest("/authapi/login", Method.POST);
            var postdata = new
            {
                username = "gswaaa",
                password = "111111",
            };
            var json = loginRequest.JsonSerializer.Serialize(postdata);
            loginRequest.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            IRestResponse loginResponse = loginClient.Execute(loginRequest);
            var loginContent = loginResponse.Content;
            Console.WriteLine(loginContent);
            return Newtonsoft.Json.JsonConvert.DeserializeObject(loginContent);
        }

        public dynamic SystemLogin()
        {
            var loginClient = new RestClient(_url);
            var loginRequest = new RestRequest("/authapi/login", Method.POST);
            var postdata = new
            {
                username = "ggg",
                password = "222222",
            };
            var json = loginRequest.JsonSerializer.Serialize(postdata);
            loginRequest.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            IRestResponse loginResponse = loginClient.Execute(loginRequest);
            var loginContent = loginResponse.Content;
            Console.WriteLine(loginContent);
            return Newtonsoft.Json.JsonConvert.DeserializeObject(loginContent);
        }
        public dynamic AdminLogin()
        {
            var loginClient = new RestClient(_url);
            var loginRequest = new RestRequest("/authapi/login", Method.POST);
            var postdata = new
            {
                username = "gsw",
                password = "111111",
            };
            var json = loginRequest.JsonSerializer.Serialize(postdata);
            loginRequest.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            IRestResponse loginResponse = loginClient.Execute(loginRequest);
            var loginContent = loginResponse.Content;
            Console.WriteLine(loginContent);
            return Newtonsoft.Json.JsonConvert.DeserializeObject(loginContent);
        }
        public void DemoAAPI(dynamic token)
        {
            var client = new RestClient(_url);
            //这里要在获取的令牌字符串前加Bearer
            string tk = "Bearer " + Convert.ToString(token?.access_token);
            client.AddDefaultHeader("Authorization", tk);
            var request = new RestRequest("/productapi/values", Method.GET);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            Console.WriteLine($"状态码：{(int)response.StatusCode} 状态信息：{response.StatusCode} 返回结果：{content}");
        }
        public void DemoBAPI(dynamic token)
        {
            var client = new RestClient(_url);
            //这里要在获取的令牌字符串前加Bearer
            string tk = "Bearer " + Convert.ToString(token?.access_token);
            client.AddDefaultHeader("Authorization", tk);
            var request = new RestRequest("/usersapi/values", Method.GET);
            IRestResponse response = client.Execute(request);
            var content = response.Content; Console.WriteLine($"状态码：{(int)response.StatusCode} 状态信息：{response.StatusCode} 返回结果：{content}");
        }
    }
}



