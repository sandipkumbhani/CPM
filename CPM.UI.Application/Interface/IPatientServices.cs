using CPM.UI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Application.Interface
{
    public interface IPatientServices
    {
        Task<PatientDto> Add(PatientDto model);
        Task<IEnumerable<PatientDto>> GetAllPatient();
        Task<PatientDto> GetById(int id);
        Task<String> UpdatePatient(int id, PatientDto model);
        Task<string> DeletePatient(int id);
    }
}
