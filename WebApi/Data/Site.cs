using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Site
    {
        public Guid Id { get; set; }
        public string SiteName { get; set; }
        public List<Doctor> DoctorList { get; set; }
        public List<PatientSite> PatientSiteList { get; set; }
        public Site()
        {
            DoctorList = new List<Doctor>();
            PatientSiteList = new List<PatientSite>();
        }
    }
    public class SiteMap
    {
        public SiteMap(EntityTypeBuilder<Site> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.SiteName).IsRequired();
            entityBuilder.HasMany(e => e.DoctorList).WithOne(e => e.Site);
            entityBuilder.HasMany(e => e.PatientSiteList).WithOne(e => e.Site);
        }
    }
}
