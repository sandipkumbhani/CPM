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
    public class RegisterServices : IRegisterServices
    {
        private readonly IRegisterAdaptor _registerAdaptor;
        public RegisterServices(IRegisterAdaptor registerAdaptor)
        {
            _registerAdaptor = registerAdaptor;
        }
        public Task<string> Register(RegisterDto model)
        {
            return  _registerAdaptor.RegisterAsync(model);
        }
    }
}
