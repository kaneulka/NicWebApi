using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Cabinet
    {
        public Guid Id { get; set; }
        public string CabinetNumber { get; set; }
        public List<Doctor> DoctorList { get; set; }
        public Cabinet()
        {
            DoctorList = new List<Doctor>();
        }
    }
    public class CabinetMap
    {
        public CabinetMap(EntityTypeBuilder<Cabinet> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.CabinetNumber).IsRequired();
            entityBuilder.HasMany(e => e.DoctorList).WithOne(e => e.Cabinet);
        }
    }
}
