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
    public class ClinicServices : IClinicServices
    {
        private readonly IClinicAdaptor _clinicAdaptor;
        public ClinicServices(IClinicAdaptor clinicAdaptor)
        {
            _clinicAdaptor = clinicAdaptor;
        }
        public async Task<string> Add(ClinicMasterDto model)
        {
            return await _clinicAdaptor.Addasync(model);
        }

        public Task<string> DeleteClinic(int id)
        {
            return _clinicAdaptor.DeleteClinicAsync(id);
        }

        public async Task<IEnumerable<ClinicMasterDto>> GetAll()
        {
            return await _clinicAdaptor.GetAllClinicAsync();
        }

        public async Task<ClinicMasterDto> GetById(int id)
        {
            return await _clinicAdaptor.GetByIdAsync(id);
        }

        public async Task<string> UpdateClinic(int id, ClinicMasterDto model)
        {
            return await _clinicAdaptor.UpdateClinicAsync(id, model);
        }
    }
}
