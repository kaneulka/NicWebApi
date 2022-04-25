using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class PatientSite
    {
        public long Id { get; set; }
        public Guid PatientId { get; set; }
        public Patient? Patient { get; set; }
        public Guid SiteId { get; set; }
        public Site? Site { get; set;}
    }
    public class PatientSiteMap
    {
        public PatientSiteMap(EntityTypeBuilder<PatientSite> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.PatientId).IsRequired();
            entityBuilder.Property(e => e.SiteId).IsRequired();
        }
    }
}
