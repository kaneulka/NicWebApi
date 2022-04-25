using Data;
using Repository;
using Repository.DoctorRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Service.DoctorService
{
    public class DoctorService : IService<DoctorViewModel>
    {
        private IRepository<Doctor> doctorRepository;

        public DoctorService(IRepository<Doctor> doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }

        public async Task<List<DoctorViewModel>> GetAll()
        {
            List<Doctor> entities = await doctorRepository.GetAll();
            List<DoctorViewModel> entitiesViewModel = new List<DoctorViewModel>();
            foreach(var entity in entities)
            {
                DoctorViewModel entityViewModel = new DoctorViewModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    CabinetNumber = entity.Cabinet.CabinetNumber,
                    SiteName = entity.Site.SiteName,
                    SpecializationName = entity.Specialization.SpecializationName
                };
                entitiesViewModel.Add(entityViewModel);
            }
            return entitiesViewModel;
        }

        public async Task<DoctorViewModel> Get(Guid id)
        {
            //if (!IsExist(id)) return null;
            Doctor entity = await doctorRepository.Get(id);
            if (entity == null) return null;
            DoctorViewModel entityViewModel = new DoctorViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                CabinetNumber = entity.Cabinet.CabinetNumber,
                SiteName = entity.Site.SiteName,
                SpecializationName = entity.Specialization.SpecializationName
            };
            return entityViewModel;
        }

        public async Task<List<DoctorViewModel>> GetSome(List<Guid> ids)
        {
            List<Doctor> entities = await doctorRepository.GetSome(ids);
            List<DoctorViewModel> entitiesViewModel = new List<DoctorViewModel>();
            foreach (var entity in entities)
            {
                DoctorViewModel entityViewModel = new DoctorViewModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    CabinetNumber = entity.Cabinet.CabinetNumber,
                    SiteName = entity.Site.SiteName,
                    SpecializationName = entity.Specialization.SpecializationName
                };
                entitiesViewModel.Add(entityViewModel);
            }
            return entitiesViewModel;
        }
        public async Task Insert(DoctorViewModel entityViewModel)
        {
            Doctor entity = new Doctor
            {
                Id = Guid.NewGuid(),
                Name = entityViewModel.Name,
                CabinetId = entityViewModel.CabinetId,
                SiteId = entityViewModel.SiteId,
                SpecializationId = entityViewModel.SpecializationId
            };
            await doctorRepository.Insert(entity);
        }
        public async Task Update(DoctorViewModel entityViewModel)
        {
            Doctor entity = await doctorRepository.Get(entityViewModel.Id);
            entity.Name = entityViewModel.Name;
            entity.CabinetId = entityViewModel.CabinetId;
            entity.SiteId = entityViewModel.SiteId;
            entity.SpecializationId = entityViewModel.SpecializationId;
            await doctorRepository.Update(entity);
        }
        public async Task Delete(Guid id)
        {
            await doctorRepository.Delete(id);
        }
        public async Task<bool> IsExist(string name)
        {
            return await doctorRepository.IsExist(name);

        }
    }
}
