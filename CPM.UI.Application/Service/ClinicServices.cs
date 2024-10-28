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
        public Task<string> Add(ClinicMasterDto model)
        {
            return _clinicAdaptor.Addasync(model);
        }
    }
}
