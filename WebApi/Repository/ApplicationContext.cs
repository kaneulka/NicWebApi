using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new CabinetMap(modelBuilder.Entity<Cabinet>());
            new DoctorMap(modelBuilder.Entity<Doctor>());
            new PatientMap(modelBuilder.Entity<Patient>());
            new SiteMap(modelBuilder.Entity<Site>());
            new SpecializationMap(modelBuilder.Entity<Specialization>());
        }
    }
}
