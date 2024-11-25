using CPM.UI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Domain.Interfaces
{
    public interface IDoctorAdaptor
    {
        Task<string> Addasync(DoctorMasterDto model);
        Task<IEnumerable<DoctorMasterDto>> GetAllDoctorAsync();
        Task<DoctorMasterDto> GetByIdAsync(int id);

        Task<string> UpdateDoctorAsync(int id, DoctorMasterDto model);

        Task<string> DeleteDoctorAsync(int id);
    }
}
