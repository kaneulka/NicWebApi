using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Service.SpecializationService
{
    public interface ISpecializationService : IService<SpecializationViewModel>
    {
        Task<SpecializationViewModel> GetByName(string name);
    }
}
