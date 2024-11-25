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
    public class PatientAdaptor : IPatientAdaptor
    {
        private readonly GlobalClass _globalClass;

        public PatientAdaptor(GlobalClass globalClass)
        {
            _globalClass = globalClass;
        }

        public async Task<PatientDto> Addasync(PatientDto model)
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var baseUrl = "https://localhost:5001/api/Patient/AddPatient";

            var patient = JsonConvert.SerializeObject(model);
            var requestContent = new StringContent(patient, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(baseUrl, requestContent);
            var responseData = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<CommanResponseDto>(responseData);
            if (response.IsSuccessStatusCode)
            {
                // Deserialize response data to PatientDto
                var addedPatient = JsonConvert.DeserializeObject<PatientDto>(Convert.ToString(responseModel.Data!));
                return addedPatient;
            }
            return null; 
        }

        public async Task<string> DeletePatientAsync(int id)
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var baseUrl = "https://localhost:5001/api/Patient/DeletePatient/" + id;

            var patient = JsonConvert.SerializeObject(id);
            var requestContent = new StringContent(patient, Encoding.UTF8, "application/json");
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

        public async Task<IEnumerable<PatientDto>> GetAllPatientAsync()
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var response = await _httpClient.GetAsync("https://localhost:5001/api/Patient/GetAllPatient");
            var responseData = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<CommanResponseDto>(responseData);
            if (responseModel != null)
            {
                var patient = JsonConvert.DeserializeObject<List<PatientDto>>(Convert.ToString(responseModel.Data!));
                return patient;
            }
            return null;
        }

        public async Task<PatientDto> GetByIdAsync(int id)
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var response = await _httpClient.GetAsync("https://localhost:5001/api/Patient/GetPatientById/" + id);
            var responseData = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<CommanResponseDto>(responseData);
            if (responseModel != null)
            {
                var patient = JsonConvert.DeserializeObject<PatientDto>(Convert.ToString(responseModel.Data!));
                return patient;
            }
            return null;
        }

        public async Task<string> UpdatePatientAsync(int id, PatientDto model)
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var baseUrl = "https://localhost:5001/api/Patient/UpdatePatient/" + id;

            var patient = JsonConvert.SerializeObject(model);
            var requestContent = new StringContent(patient, Encoding.UTF8, "application/json");
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
