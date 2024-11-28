using CPM.UI.Domain.Interfaces;
using CPM.UI.Domain.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace CPM.UI.Inftrastucture.Provider
{
    public class RegisterAdaptor : IRegisterAdaptor
    {
        private HttpClient _httpClient;
        public async Task<string> RegisterAsync(RegisterDto model)
        {
             _httpClient = new HttpClient();
            var baseUrl = "https://localhost:5001/api/Register";  
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
    }
}
