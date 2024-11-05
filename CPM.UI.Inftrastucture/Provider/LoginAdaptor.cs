using CPM.UI.Domain.Interfaces;
using CPM.UI.Domain.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Inftrastucture.Provider
{
    public class LoginAdaptor : ILoginAdaptor
    {
        private readonly HttpClient _httpClient;

        public LoginAdaptor(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> PostApiDataAsync(LoginViewModel loginViewModel)
        {
            try
            {
                // _httpClient = new HttpClient();
                var baseUrl = "https://localhost:5001/api/Login";

                var user = JsonConvert.SerializeObject(loginViewModel);
                var requestContent = new StringContent(user, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(baseUrl, requestContent);
                var responseData = await response.Content.ReadAsStringAsync();
                var responseModel = JsonConvert.DeserializeObject<CommanResponseDto>(responseData);
                if (responseModel != null)
                {
                    var responseToken = JsonConvert.DeserializeObject<ResponseToken>(responseModel?.Data.ToString()!);
                    //var dataJson = JsonConvert.SerializeObject(responseModel.Data);
                    //var responseToken = JsonConvert.DeserializeObject<ResponseToken>(dataJson);
                    return responseToken.Token;

                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public class ResponseToken
        {
            public string? Token { get; set; }
        }


    }
}

