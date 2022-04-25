using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CabinetId { get; set; }
        public Cabinet Cabinet { get; set; }
        public Guid SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
        public Guid SiteId { get; set; }
        public Site Site { get; set; }
    }
    public class DoctorMap
    {
        public DoctorMap(EntityTypeBuilder<Doctor> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.Name).IsRequired();
            entityBuilder.Property(e => e.CabinetId).IsRequired();
            entityBuilder.Property(e => e.SpecializationId).IsRequired();
            entityBuilder.Property(e => e.SiteId).IsRequired();
        }
    }
}
