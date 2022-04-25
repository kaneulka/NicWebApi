using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.PatientSiteRepository
{
    public interface IPatientSiteRepository
    {
        Task<List<PatientSite>> GetAll();
        Task<List<PatientSite>> GetById(Guid patientId);
        Task InsertArray(List<PatientSite> entities);
        Task DeleteArray(List<PatientSite> entities);
        Task SaveChangesAsync();
    }
}
