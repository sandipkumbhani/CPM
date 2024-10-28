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
        public Task<string> Add(DoctorMasterDto model)
        {
           return _doctorAdaptor.Addasync(model);
        }
    }
}
