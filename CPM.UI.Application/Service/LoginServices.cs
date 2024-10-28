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
    public class LoginServices : ILoginServices
    {
        private readonly ILoginAdaptor _loginAdaptor;
        public LoginServices(ILoginAdaptor loginAdaptor)
        {
            _loginAdaptor = loginAdaptor;
        }
        public Task<string> Login(LoginViewModel model)
        {
            return _loginAdaptor.PostApiDataAsync(model);
        }
    }
}
