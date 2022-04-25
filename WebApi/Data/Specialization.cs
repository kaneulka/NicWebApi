using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Specialization
    {
        public Guid Id { get; set; }
        public string SpecializationName { get; set; }
        public List<Doctor> DoctorList { get; set; }
        public Specialization()
        {
            DoctorList = new List<Doctor>();
        }
    }
    public class SpecializationMap
    {
        public SpecializationMap(EntityTypeBuilder<Specialization> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.SpecializationName).IsRequired();
            entityBuilder.HasMany(e => e.DoctorList).WithOne(e => e.Specialization);
        }
    }
}
