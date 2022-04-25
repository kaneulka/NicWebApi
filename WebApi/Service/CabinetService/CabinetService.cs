using Data;
using Repository;
using Repository.CabinetRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Service.CabinetService
{
    public class CabinetService : ICabinetService
    {
        private IRepository<Cabinet> cabinetRepository;

        public CabinetService(IRepository<Cabinet> cabinetRepository)
        {
            this.cabinetRepository = cabinetRepository;
        }

        public async Task<List<CabinetViewModel>> GetAll()
        {
            List<Cabinet> entities = await cabinetRepository.GetAll();
            List<CabinetViewModel> entitiesViewModel = new List<CabinetViewModel>();
            foreach(var entity in entities)
            {
                CabinetViewModel entityViewModel = new CabinetViewModel
                {
                    Id = entity.Id,
                    CabinetNumber = entity.CabinetNumber
                };
                entitiesViewModel.Add(entityViewModel);
            }
            return entitiesViewModel;
        }

        public async Task<CabinetViewModel> Get(Guid id)
        {
            Cabinet entity = await cabinetRepository.Get(id);
            CabinetViewModel entityViewModel = new CabinetViewModel
            {
                Id = entity.Id,
                CabinetNumber = entity.CabinetNumber
            };
            return entityViewModel;
        }

        public async Task< List<CabinetViewModel>> GetSome(List<Guid> ids)
        {
            List<Cabinet> entities = await cabinetRepository.GetSome(ids);
            List<CabinetViewModel> entitiesViewModel = new List<CabinetViewModel>();
            foreach (var entity in entities)
            {
                CabinetViewModel entityViewModel = new CabinetViewModel
                {
                    Id = entity.Id,
                    CabinetNumber = entity.CabinetNumber
                };
                entitiesViewModel.Add(entityViewModel);
            }
            return entitiesViewModel;
        }
        public async Task Insert(CabinetViewModel entityViewModel)
        {
            Cabinet entity = new Cabinet
            {
                Id = Guid.NewGuid(),
                CabinetNumber = entityViewModel.CabinetNumber
            };
            await cabinetRepository.Insert(entity);
        }
        public async Task Update(CabinetViewModel entityViewModel)
        {
            Cabinet entity = await cabinetRepository.Get(entityViewModel.Id);
            entity.CabinetNumber = entityViewModel.CabinetNumber;
            await cabinetRepository.Update(entity);
        }
        public async Task Delete(Guid id)
        {
            await cabinetRepository.Delete(id);
        }
        public async Task<bool> IsExist(string name)
        {
            return await cabinetRepository.IsExist(name);

        }
        public async Task<CabinetViewModel> GetByName(string name)
        {
            List<CabinetViewModel> entity = await GetAll();
            return entity.FirstOrDefault(e=> e.CabinetNumber == name);
        }
    }
}
