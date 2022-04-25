using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.CabinetRepository
{
    public class CabinetRepository : IRepository<Cabinet>
    {
        private readonly ApplicationContext context;
        private DbSet<Cabinet> entities;
        public CabinetRepository(ApplicationContext context)
        {
            this.context = context;
            entities = context.Set<Cabinet>();
        }

        public async Task<List<Cabinet>> GetAll()
        {
            return await entities.ToListAsync();
        }
        public async Task<Cabinet> Get(Guid id)
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<List<Cabinet>> GetSome(List<Guid> ids)
        {
            return await entities.Where(e=> ids.Contains(e.Id)).ToListAsync();
        }
        public async Task Insert(Cabinet entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            await entities.AddAsync(entity);
            await context.SaveChangesAsync();
        }
        public async Task Update(Cabinet entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            await context.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            Cabinet entity = await entities.SingleOrDefaultAsync(s => s.Id == id);
            if (entity == null)
                throw new ArgumentNullException("entity");
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task Remove(Cabinet entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<bool> IsExist(string name)
        {
            var entity = await GetAll();
            if(entity.SingleOrDefault(e=> e.CabinetNumber == name ) != null) { return true; } else { return false; }
        }
    }
}
