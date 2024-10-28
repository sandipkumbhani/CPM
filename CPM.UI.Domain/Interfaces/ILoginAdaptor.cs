using CPM.UI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Domain.Interfaces
{
    public interface ILoginAdaptor
    {
        Task<string> PostApiDataAsync(LoginViewModel model);
    }
}
