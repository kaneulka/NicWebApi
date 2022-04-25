using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SpecializationRepository
{
    public class SpecializationRepository : IRepository<Specialization>
    {
        private readonly ApplicationContext context;
        private DbSet<Specialization> entities;
        public SpecializationRepository(ApplicationContext context)
        {
            this.context = context;
            entities = context.Set<Specialization>();
        }

        public async Task <List<Specialization>> GetAll()
        {
            return await entities.ToListAsync();
        }
        public async Task <Specialization> Get(Guid id)
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<List<Specialization>> GetSome(List<Guid> ids)
        {
            return await entities.Where(e=> ids.Contains(e.Id)).ToListAsync();
        }
        public async Task Insert(Specialization entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            await entities.AddAsync(entity);
            await context.SaveChangesAsync();
        }
        public async Task Update(Specialization entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            await context.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            Specialization entity = await entities.SingleOrDefaultAsync(s => s.Id == id);
            if (entity == null)
                throw new ArgumentNullException("entity");
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task Remove(Specialization entity)
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
            if (entity.SingleOrDefault(e => e.SpecializationName == name) != null) { return true; } else { return false; }
        }
    }
}
