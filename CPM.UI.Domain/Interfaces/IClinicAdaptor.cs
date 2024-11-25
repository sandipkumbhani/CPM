using CPM.UI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Domain.Interfaces
{
    public interface IClinicAdaptor
    {
        Task<string> Addasync(ClinicMasterDto model);
        Task<IEnumerable<ClinicMasterDto>> GetAllClinicAsync();

        Task<ClinicMasterDto> GetByIdAsync(int id);

        Task<string> UpdateClinicAsync(int id, ClinicMasterDto model);

        Task<string> DeleteClinicAsync(int id);
    }
}
