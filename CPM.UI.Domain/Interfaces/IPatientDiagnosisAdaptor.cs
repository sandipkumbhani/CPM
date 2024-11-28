using CPM.UI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Domain.Interfaces
{
    public interface IPatientDiagnosisAdaptor
    {
        Task<string> Addasync(PatientDiagnosisDto model);
        Task<IEnumerable<PatientDiagnosisDto>> GetAllAsync();
        Task<PatientDiagnosisDto> GetByIdAsync(int id);

        Task<string> UpdateAsync(int id, PatientDiagnosisDto model);

        Task<string> DeleteAsync(int id);

    }
}
