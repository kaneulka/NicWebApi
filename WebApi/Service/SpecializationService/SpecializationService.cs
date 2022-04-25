using Data;
using Repository;
using Repository.SpecializationRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Service.SpecializationService
{
    public class SpecializationService : ISpecializationService
    {
        private IRepository<Specialization> specializationRepository;

        public SpecializationService(IRepository<Specialization> specializationRepository)
        {
            this.specializationRepository = specializationRepository;
        }

        public async Task<List<SpecializationViewModel>> GetAll()
        {
            List<Specialization> entities = await specializationRepository.GetAll();
            List<SpecializationViewModel> entitiesViewModel = new List<SpecializationViewModel>();
            foreach(var entity in entities)
            {
                SpecializationViewModel entityViewModel = new SpecializationViewModel
                {
                    Id = entity.Id,
                    SpecializationName = entity.SpecializationName
                };
                entitiesViewModel.Add(entityViewModel);
            }
            return entitiesViewModel;
        }

        public async Task<SpecializationViewModel> Get(Guid id)
        {
            Specialization entity = await specializationRepository.Get(id);
            SpecializationViewModel entityViewModel = new SpecializationViewModel
            {
                Id = entity.Id,
                SpecializationName = entity.SpecializationName
            };
            return entityViewModel;
        }

        public async Task<List<SpecializationViewModel>> GetSome(List<Guid> ids)
        {
            List<Specialization> entities = await specializationRepository.GetSome(ids);
            List<SpecializationViewModel> entitiesViewModel = new List<SpecializationViewModel>();
            foreach (var entity in entities)
            {
                SpecializationViewModel entityViewModel = new SpecializationViewModel
                {
                    Id = entity.Id,
                    SpecializationName = entity.SpecializationName
                };
                entitiesViewModel.Add(entityViewModel);
            }
            return entitiesViewModel;
        }
        public async Task Insert(SpecializationViewModel entityViewModel)
        {
            Specialization entity = new Specialization
            {
                Id = Guid.NewGuid(),
                SpecializationName = entityViewModel.SpecializationName
            };
            await specializationRepository.Insert(entity);
        }
        public async Task Update(SpecializationViewModel entityViewModel)
        {
            Specialization entity = await specializationRepository.Get(entityViewModel.Id);
            entity.SpecializationName = entityViewModel.SpecializationName;
            await specializationRepository.Update(entity);
        }
        public async Task Delete(Guid id)
        {
            await specializationRepository.Delete(id);
        }
        public async Task<bool> IsExist(string name)
        {
            return await specializationRepository.IsExist(name);

        }
        public async Task<SpecializationViewModel> GetByName(string name)
        {
            List<SpecializationViewModel> entity = await GetAll();
            return entity.FirstOrDefault(e => e.SpecializationName == name);
        }
    }
}
