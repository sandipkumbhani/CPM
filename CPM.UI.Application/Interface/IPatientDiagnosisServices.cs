using CPM.UI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Application.Interface
{
    public interface IPatientDiagnosisServices
    {
        Task<string> Add(PatientDiagnosisDto model);
        Task<IEnumerable<PatientDiagnosisDto>> GetAll();
        Task<PatientDiagnosisDto> GetById(int id);
        Task<String> Update(int id, PatientDiagnosisDto model);
        Task<string> Delete(int id);
    }
}
