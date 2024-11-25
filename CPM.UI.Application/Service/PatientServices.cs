using CPM.UI.Application.Interface;
using CPM.UI.Domain.Interfaces;
using CPM.UI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Application.Service
{
    public class PatientServices : IPatientServices
    {
        private readonly IPatientAdaptor _patientAdaptor;

        public PatientServices(IPatientAdaptor patientAdaptor)
        {
            _patientAdaptor = patientAdaptor;
        }

        public async Task<PatientDto> Add(PatientDto model)
        {
            return await _patientAdaptor.Addasync(model);
        }

        public  async Task<string> DeletePatient(int id)
        {
            return await _patientAdaptor.DeletePatientAsync(id);
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatient()
        {
            return await _patientAdaptor.GetAllPatientAsync();
        }

        public async Task<PatientDto> GetById(int id)
        {
            return await _patientAdaptor.GetByIdAsync(id);
        }

        public async Task<string> UpdatePatient(int id, PatientDto model)
        {
            return await _patientAdaptor.UpdatePatientAsync(id, model); 
        }
    }
}
