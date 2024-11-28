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
    public class PatientDiagnosisAdaptor : IPatientDiagnosisAdaptor
    {
        private readonly GlobalClass _globalClass;

        public PatientDiagnosisAdaptor(GlobalClass globalClass)
        {
            _globalClass = globalClass;
        }

        public async Task<string> Addasync(PatientDiagnosisDto model)
        {

            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var baseUrl = "https://localhost:5001/api/PatientDignosis/AddDiagnosis";

            var Diagnosis = JsonConvert.SerializeObject(model);
            var requestContent = new StringContent(Diagnosis, Encoding.UTF8, "application/json");
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

        public async Task<string> DeleteAsync(int id)
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var baseUrl = "https://localhost:5001/api/PatientDignosis/DeleteDiagnosis" + id;

            var Diagnosis = JsonConvert.SerializeObject(id);
            var requestContent = new StringContent(Diagnosis, Encoding.UTF8, "application/json");
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


    public async Task<IEnumerable<PatientDiagnosisDto>> GetAllAsync()
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var response = await _httpClient.GetAsync("https://localhost:5001/api/PatientDignosis/GetAllDignosis");
            var responseData = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<CommanResponseDto>(responseData);
            if (responseModel != null)
            {
                var Diagnosis = JsonConvert.DeserializeObject<List<PatientDiagnosisDto>>(Convert.ToString(responseModel.Data!));
                return Diagnosis;
            }
            return null;
        }

        public async Task<PatientDiagnosisDto> GetByIdAsync(int id)
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var response = await _httpClient.GetAsync("https://localhost:5001/api/PatientDignosis/GetDignosisById/" + id);
            var responseData = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<CommanResponseDto>(responseData);
            if (responseModel != null)
            {
                var Diagnosis = JsonConvert.DeserializeObject<PatientDiagnosisDto>(Convert.ToString(responseModel.Data!));
                return Diagnosis;
            }
            return null;
        }

        public async Task<string> UpdateAsync(int id, PatientDiagnosisDto model)
        {
            var _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _globalClass.Token);
            var baseUrl = "https://localhost:5001/api/PatientDignosis/UpdateDignosis/" + id;

            var Diagnosis = JsonConvert.SerializeObject(model);
            var requestContent = new StringContent(Diagnosis, Encoding.UTF8, "application/json");
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
