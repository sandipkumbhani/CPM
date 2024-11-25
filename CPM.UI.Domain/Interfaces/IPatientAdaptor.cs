using CPM.UI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Domain.Interfaces
{
    public interface IPatientAdaptor
    {
        Task<PatientDto> Addasync(PatientDto model);
        Task<IEnumerable<PatientDto>> GetAllPatientAsync();
        Task<PatientDto> GetByIdAsync(int id);

        Task<string> UpdatePatientAsync(int id, PatientDto model);

        Task<string> DeletePatientAsync(int id); 
    }
}
