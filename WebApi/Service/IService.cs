using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IService<T>
    {
        Task<List<T>> GetAll();
        Task<T> Get(Guid id);
        Task<List<T>> GetSome(List<Guid> ids);
        Task Insert(T entityViewModel);
        Task Update(T entityViewModel);
        Task Delete(Guid id);
        Task<bool> IsExist(string name);
    }
}
