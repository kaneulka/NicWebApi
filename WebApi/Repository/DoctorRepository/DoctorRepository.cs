using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DoctorRepository
{
    public class DoctorRepository : IRepository<Doctor>
    {
        private readonly ApplicationContext context;
        private DbSet<Doctor> entities;
        public DoctorRepository(ApplicationContext context)
        {
            this.context = context;
            entities = context.Set<Doctor>();
        }

        public async Task<List<Doctor>> GetAll()
        {
            return await entities
                .Include(e => e.Cabinet)
                .Include(e => e.Site)
                .Include(e => e.Specialization)
                .ToListAsync();
        }
        public async Task<Doctor> Get(Guid id)
        {
            return await entities
                .Include(e => e.Cabinet)
                .Include(e => e.Site)
                .Include(e => e.Specialization)
                .SingleOrDefaultAsync(s => s.Id == id);
        }
        public  async Task<List<Doctor>> GetSome(List<Guid> ids)
        {
            return await entities
                .Include(e => e.Cabinet)
                .Include(e => e.Site)
                .Include(e => e.Specialization)
                .Where(e=> ids.Contains(e.Id)).ToListAsync();
        }
        public async Task Insert(Doctor entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            await entities.AddAsync(entity);
            await context.SaveChangesAsync();
        }
        public async Task Update(Doctor entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            await context.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            Doctor entity = await entities.SingleOrDefaultAsync(s => s.Id == id);
            if (entity == null)
                throw new ArgumentNullException("entity");
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task Remove(Doctor entity)
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
            if (entity.SingleOrDefault(e => e.Name == name) != null) { return true; } else { return false; }
        }
    }
}
