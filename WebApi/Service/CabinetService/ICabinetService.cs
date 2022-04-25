using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Service.CabinetService
{
    public interface ICabinetService : IService<CabinetViewModel>
    {
        Task<CabinetViewModel> GetByName(string name);
    }
}
