using Data;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Service.SiteService
{
    public class SiteService : ISiteService
    {
        private IRepository<Site> siteRepository;

        public SiteService(IRepository<Site> siteRepository)
        {
            this.siteRepository = siteRepository;
        }

        public async Task<List<SiteViewModel>> GetAll()
        {
            List<Site> entities = await siteRepository.GetAll();
            List<SiteViewModel> entitiesViewModel = new List<SiteViewModel>();
            foreach(var entity in entities)
            {
                SiteViewModel entityViewModel = new SiteViewModel
                {
                    Id = entity.Id,
                    SiteName = entity.SiteName
                };
                entitiesViewModel.Add(entityViewModel);
            }
            return entitiesViewModel;
        }

        public async Task<SiteViewModel> Get(Guid id)
        {
            Site entity = await siteRepository.Get(id);
            SiteViewModel entityViewModel = new SiteViewModel
            {
                Id = entity.Id,
                SiteName = entity.SiteName
            };
            return entityViewModel;
        }

        public async Task<List<SiteViewModel>> GetSome(List<Guid> ids)
        {
            List<Site> entities = await siteRepository.GetSome(ids);
            List<SiteViewModel> entitiesViewModel = new List<SiteViewModel>();
            foreach (var entity in entities)
            {
                SiteViewModel entityViewModel = new SiteViewModel
                {
                    Id = entity.Id,
                    SiteName = entity.SiteName
                };
                entitiesViewModel.Add(entityViewModel);
            }
            return entitiesViewModel;
        }
        public async Task Insert(SiteViewModel entityViewModel)
        {
            Site entity = new Site
            {
                Id = Guid.NewGuid(),
                SiteName = entityViewModel.SiteName
            };
            await siteRepository.Insert(entity);
        }
        public async Task Update(SiteViewModel entityViewModel)
        {
            Site entity = await siteRepository.Get(entityViewModel.Id);
            entity.SiteName = entityViewModel.SiteName;
            await siteRepository.Update(entity);
        }
        public async Task Delete(Guid id)
        {
            await siteRepository.Delete(id);
        }
        public async Task<bool> IsExist(string name)
        {
            return await siteRepository.IsExist(name);

        }
        public async Task<SiteViewModel> GetByName(string name)
        {
            List<SiteViewModel> entity = await GetAll();
            return entity.FirstOrDefault(e => e.SiteName == name);
        }
    }
}
