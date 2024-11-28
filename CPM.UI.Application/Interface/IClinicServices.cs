using CPM.UI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Application.Interface
{
    public interface IClinicServices
    {
        Task<string> Add(ClinicMasterDto model);
        Task<IEnumerable<ClinicMasterDto>> GetAll();

        Task<ClinicMasterDto> GetById(int id);
        Task<String> UpdateClinic(int id, ClinicMasterDto model);
        Task<string> DeleteClinic(int id);
    }
}
