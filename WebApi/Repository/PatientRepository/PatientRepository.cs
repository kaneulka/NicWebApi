using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.PatientRepository
{
    public class PatientRepository : IRepository<Patient>
    {
        private readonly ApplicationContext context;
        private DbSet<Patient> entities;
        public PatientRepository(ApplicationContext context)
        {
            this.context = context;
            entities = context.Set<Patient>();
        }

        public async Task<List<Patient>> GetAll()
        {
            return await entities.ToListAsync();
        }
        public async Task<Patient> Get(Guid id)
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<List<Patient>> GetSome(List<Guid> ids)
        {
            return await entities.Where(e=> ids.Contains(e.Id)).ToListAsync();
        }
        public async Task Insert(Patient entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            await entities.AddAsync(entity);
            await context.SaveChangesAsync();
        }
        public async Task Update(Patient entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            await context.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            Patient entity = await entities.SingleOrDefaultAsync(s => s.Id == id);
            if (entity == null)
                throw new ArgumentNullException("entity");
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task Remove(Patient entity)
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
            List<string> names = name.Split(' ').ToList();
            if (entity.SingleOrDefault(e => e.FirstName == name[0].ToString() && e.SecondName == name[1].ToString()) != null) { return true; } else { return false; }
        }
    }
}
