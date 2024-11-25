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
    public class PatientDiagnosisServices : IPatientDiagnosisServices
    {
        private readonly IPatientDiagnosisAdaptor _patientDiagnosisAdaptor;

        public PatientDiagnosisServices(IPatientDiagnosisAdaptor patientDiagnosisAdaptor)
        {
            _patientDiagnosisAdaptor = patientDiagnosisAdaptor;
        }

        public async Task<string> Add(PatientDiagnosisDto model)
        {
            return await _patientDiagnosisAdaptor.Addasync(model);
        }

        public async Task<string> Delete(int id)
        {
            return await _patientDiagnosisAdaptor.DeleteAsync(id);  
        }

        public async Task<IEnumerable<PatientDiagnosisDto>> GetAll()
        {
            return await _patientDiagnosisAdaptor.GetAllAsync();
        }

        public async Task<PatientDiagnosisDto> GetById(int id)
        {
            return await _patientDiagnosisAdaptor.GetByIdAsync(id);  
        }

        public async Task<string> Update(int id, PatientDiagnosisDto model)
        {

            return await _patientDiagnosisAdaptor.UpdateAsync(id,model);   
        }
    }
}
