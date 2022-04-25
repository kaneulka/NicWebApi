using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> Get(Guid id);
        Task<List<T>> GetSome(List<Guid> ids);
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
        Task Remove(T entity);
        Task SaveChangesAsync();
        Task<bool> IsExist(string name);
    }
}
