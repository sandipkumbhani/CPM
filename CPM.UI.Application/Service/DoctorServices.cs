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
    public class DoctorServices : IDoctorServices
    {
        private readonly IDoctorAdaptor _doctorAdaptor;
        public DoctorServices(IDoctorAdaptor doctorAdaptor)
        {
            _doctorAdaptor = doctorAdaptor;
        }
        public async Task<string> Add(DoctorMasterDto model)
        {
           return await _doctorAdaptor.Addasync(model);
        }

        public async Task<string> DeleteDoctor(int id)
        {
            return await _doctorAdaptor.DeleteDoctorAsync(id);
        }

        public async Task<IEnumerable<DoctorMasterDto>> GetAllDoctor()
        {
           return await _doctorAdaptor.GetAllDoctorAsync();
        }

        public async Task<DoctorMasterDto> GetById(int id)
        {
            return await _doctorAdaptor.GetByIdAsync(id);
        }


        public async Task<string> UpdateDoctor(int id, DoctorMasterDto model)
        {
            return await _doctorAdaptor.UpdateDoctorAsync(id, model);   
        }
    }
}
