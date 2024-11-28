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
    public class ClinicAdaptor : IClinicAdaptor
    {
        private readonly GlobalClass _globalClass;

        public ClinicAdaptor(GlobalClass globalClass)
        {
            _globalClass = globalClass;
        }
        public async Task<string> Addasync(ClinicMasterDto model)
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var baseUrl = "https://localhost:7272/api/CompanyMasterItem/AddMasterItem";

            var clinic = JsonConvert.SerializeObject(model);
            var requestContent = new StringContent(clinic, Encoding.UTF8, "application/json");
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

        public async Task<string> DeleteClinicAsync(int id)
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var baseUrl = "https://localhost:5001/api/Clinic/DeleteClinic/" + id;

            var clinic = JsonConvert.SerializeObject(id);
            var requestContent = new StringContent(clinic, Encoding.UTF8, "application/json");
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

        public async Task<IEnumerable<ClinicMasterDto>> GetAllClinicAsync()
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var response = await _httpClient.GetAsync("https://localhost:5001/api/Clinic/GetAllClinic");
            var responseData = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<CommanResponseDto>(responseData);
            if (responseModel != null)
            {
                var details = JsonConvert.DeserializeObject<List<ClinicMasterDto>>(Convert.ToString(responseModel.Data!));
                return details;
            }
            return null;
        }

        public async Task<ClinicMasterDto> GetByIdAsync(int id)
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var response = await _httpClient.GetAsync("https://localhost:5001/api/Clinic/GetClinicById/"+id);
            var responseData = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<CommanResponseDto>(responseData);
            if (responseModel != null)
            {
                var details = JsonConvert.DeserializeObject<ClinicMasterDto>(Convert.ToString(responseModel.Data!));
                return details;
            }
            return null;
        }

        public async Task<string> UpdateClinicAsync(int id, ClinicMasterDto model)
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var baseUrl = "https://localhost:5001/api/Clinic/UpdateClinic/" + id;

            var clinic = JsonConvert.SerializeObject(model);
            var requestContent = new StringContent(clinic, Encoding.UTF8, "application/json");
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
    }
}
