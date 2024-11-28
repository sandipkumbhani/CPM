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
        private readonly GlobalClass _globalClass;


        public LoginAdaptor(HttpClient httpClient, GlobalClass globalClass)
        {
            _httpClient = httpClient;
            _globalClass = globalClass;
        }

        public async Task<string> AddUserAsync(LoginDto model)
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var baseUrl = "https://localhost:5001/api/Login/AddUser";
            var user = JsonConvert.SerializeObject(model);
            var requestContent = new StringContent(user, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(baseUrl, requestContent);
            var responseData = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<CommanResponseDto>(responseData);
            if (responseModel != null)
            {
                var result = responseModel.StatusCode;
                return "Success";
            }
            return null;
        }

        public async Task<LoginDto> GetByEmailAsync(string email)
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var response = await _httpClient.GetAsync("https://localhost:5001/api/Login/GetByEmail/" + email);
            var responseData = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<CommanResponseDto>(responseData);
            if (responseModel != null && responseModel.StatusCode==200)
            {
                var user = JsonConvert.DeserializeObject<LoginDto>(Convert.ToString(responseModel.Data!));
                return user;
            }

            return null;
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

        public async Task<string> UpdateUserAsync(int id, LoginDto model)
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var baseUrl = "https://localhost:5001/api/Login/UpdateUser/" + id;

            var user = JsonConvert.SerializeObject(model);
            var requestContent = new StringContent(user, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(baseUrl, requestContent);
            var responseData = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<CommanResponseDto>(responseData);
            if (responseModel != null)
            {
                var result = responseModel.StatusCode;
                return "Success";
            }

            return null;
        }

        public class ResponseToken
        {
            public string? Token { get; set; }
        }


    }
}

