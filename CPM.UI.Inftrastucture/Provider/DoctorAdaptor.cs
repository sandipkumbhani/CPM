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
    public class DoctorAdaptor : IDoctorAdaptor
    {
        private readonly GlobalClass _globalClass;

        public DoctorAdaptor(GlobalClass globalClass)
        {
            _globalClass = globalClass;
        }

        public async Task<string> Addasync(DoctorMasterDto model)
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var baseUrl = "https://localhost:5001/api/Doctor/AddDoctor";

            var doctor = JsonConvert.SerializeObject(model);
            var requestContent = new StringContent(doctor, Encoding.UTF8, "application/json");
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

        public async Task<string> DeleteDoctorAsync(int id)
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var baseUrl = "https://localhost:5001/api/Doctor/DeleteDoctor/" + id;

            var doctor = JsonConvert.SerializeObject(id);
            var requestContent = new StringContent(doctor, Encoding.UTF8, "application/json");
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

        public async Task<IEnumerable<DoctorMasterDto>> GetAllDoctorAsync()
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var response = await _httpClient.GetAsync("https://localhost:5001/api/Doctor/GetAllDoctor");
            var responseData = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<CommanResponseDto>(responseData);
            if (responseModel != null)
            {
                var Doctor = JsonConvert.DeserializeObject<List<DoctorMasterDto>>(Convert.ToString(responseModel.Data!));
                return Doctor;
            }
            return null;
        }

        public async Task<DoctorMasterDto> GetByIdAsync(int id)
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var response = await _httpClient.GetAsync("https://localhost:5001/api/Doctor/GetDoctorById/" + id);
            var responseData = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<CommanResponseDto>(responseData);
            if (responseModel != null)
            {
                var details = JsonConvert.DeserializeObject<DoctorMasterDto>(Convert.ToString(responseModel.Data!));
                return details;
            }
            return null;
        }

        public async Task<string> UpdateDoctorAsync(int id, DoctorMasterDto model)
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var baseUrl = "https://localhost:5001/api/Doctor/UpdateDoctor/" + id;

            var doctor = JsonConvert.SerializeObject(model);
            var requestContent = new StringContent(doctor, Encoding.UTF8, "application/json");
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
