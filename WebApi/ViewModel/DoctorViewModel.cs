using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class DoctorViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CabinetId { get; set; }
        public string CabinetNumber { get; set; }
        public Guid SpecializationId { get; set; }
        public string SpecializationName { get; set; }
        public Guid SiteId { get; set; }
        public string SiteName { get; set; }
    }
}
