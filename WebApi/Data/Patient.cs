using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public List<PatientSite> PatientSiteList { get; set; }
        public Patient()
        {
            PatientSiteList = new List<PatientSite>();
        }
    }
    public class PatientMap
    {
        public PatientMap(EntityTypeBuilder<Patient> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.FirstName).IsRequired();
            entityBuilder.Property(e => e.SecondName).IsRequired();
            entityBuilder.Property(e => e.Patronymic).IsRequired();
            entityBuilder.Property(e => e.Address).IsRequired();
            entityBuilder.Property(e => e.Birthday).IsRequired();
            entityBuilder.Property(e => e.Gender).IsRequired();
            entityBuilder.HasMany(e => e.PatientSiteList).WithOne(e => e.Patient);
        }
    }
}
