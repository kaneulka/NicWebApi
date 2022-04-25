using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SitetRepository
{
    public class SiteRepository : IRepository<Site>
    {
        private readonly ApplicationContext context;
        private DbSet<Site> entities;
        public SiteRepository(ApplicationContext context)
        {
            this.context = context;
            entities = context.Set<Site>();
        }

        public async Task<List<Site>> GetAll()
        {
            return await entities.ToListAsync();
        }
        public async Task<Site> Get(Guid id)
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<List<Site>> GetSome(List<Guid> ids)
        {
            return await entities.Where(e => ids.Contains(e.Id)).ToListAsync();
        }
        public async Task Insert(Site entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            await entities.AddAsync(entity);
            await context.SaveChangesAsync();
        }
        public async Task Update(Site entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            await context.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            Site entity = await entities.SingleOrDefaultAsync(s => s.Id == id);
            if (entity == null)
                throw new ArgumentNullException("entity");
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task Remove(Site entity)
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
            if (entity.SingleOrDefault(e => e.SiteName == name) != null) { return true; } else { return false; }
        }
    }
}
