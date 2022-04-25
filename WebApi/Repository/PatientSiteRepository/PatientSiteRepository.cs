using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.PatientSiteRepository
{
    public class PatientSiteRepository : IPatientSiteRepository
    {
        private readonly ApplicationContext context;
        private DbSet<PatientSite> entities;
        public PatientSiteRepository(ApplicationContext context)
        {
            this.context = context;
            entities = context.Set<PatientSite>();
        }

        public async Task<List<PatientSite>> GetAll()
        {
            return await entities.ToListAsync();
        }
        public async Task<List<PatientSite>> GetById(Guid patientId)
        {
            return await entities.Where(e=> e.PatientId == patientId).ToListAsync();
        }
        public async Task InsertArray(List<PatientSite> entitiesArray)
        {
            if (entitiesArray == null)
                throw new ArgumentNullException("entity");
            await entities.AddRangeAsync(entitiesArray.AsEnumerable());
            await context.SaveChangesAsync();
        }
        public async Task DeleteArray(List<PatientSite> entitiesArray)
        {
            if (entitiesArray == null)
                throw new ArgumentNullException("entity");
            entities.RemoveRange(entitiesArray);
            await context.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
