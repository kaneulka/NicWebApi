using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Service.SiteService
{
    public interface ISiteService : IService<SiteViewModel>
    {
        Task<SiteViewModel> GetByName(string name);
    }
}
