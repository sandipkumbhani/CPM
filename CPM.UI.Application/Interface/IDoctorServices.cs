using CPM.UI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Application.Interface
{
    public interface IDoctorServices
    {
        Task<string> Add(DoctorMasterDto model);
        Task<IEnumerable<DoctorMasterDto>> GetAllDoctor();
        Task<DoctorMasterDto> GetById(int id);
        Task<String> UpdateDoctor(int id, DoctorMasterDto model);
        Task<string> DeleteDoctor(int id);

    }
}
