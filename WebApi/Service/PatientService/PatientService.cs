using Data;
using Repository;
using Repository.PatientRepository;
using Repository.PatientSiteRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Service.PatientService
{
    public class PatientService : IService<PatientViewModel>
    {
        private IRepository<Patient> patientRepository;
        private IPatientSiteRepository patientSiteRepository;
        private IRepository<Site> siteRepository;

        public PatientService(IRepository<Patient> patientRepository, IPatientSiteRepository patientSiteRepository, IRepository<Site> siteRepository)
        {
            this.patientRepository = patientRepository;
            this.patientSiteRepository = patientSiteRepository;
            this.siteRepository = siteRepository;
        }

        public async Task< List<PatientViewModel>> GetAll()
        {
            List<Patient> entities = await patientRepository.GetAll();
            List<Site> siteEntities = await siteRepository.GetAll();
            List<PatientSite> psList = await patientSiteRepository.GetAll();
            List<PatientViewModel> entitiesViewModel = new List<PatientViewModel>();
            foreach(var entity in entities)
            {
                PatientViewModel entityViewModel = new PatientViewModel
                {
                    Id = entity.Id,
                    FirstName = entity.FirstName,
                    SecondName = entity.SecondName,
                    Patronymic = entity.Patronymic,
                    Address = entity.Address,
                    Birthday = entity.Birthday,
                    Gender = entity.Gender,
                    SiteNames = ""
                    
                };
                List<PatientSite> patientSiteEntities = psList.Where(ps => ps.PatientId == entityViewModel.Id).ToList();
                //List<SiteViewModel> lsvm = new List<SiteViewModel>();
                entityViewModel.SiteNames = String.Join(", ", siteEntities.Where(s => patientSiteEntities.Select(ps => ps.SiteId).Contains(s.Id)).Select(s => s.SiteName));
                //foreach (var siteEntity in siteEntities.Where(s=> patientSiteEntities.Select(ps=> ps.SiteId).Contains(s.Id)))
                //{
                //    //lsvm.Add(new SiteViewModel
                //    //{
                //    //    Id = siteEntity.Id,
                //    //    SiteName = siteEntity.SiteName
                //    //});
                //}
                //entityViewModel.SiteNames = lsvm;
                entitiesViewModel.Add(entityViewModel);
            }
            return entitiesViewModel;
        }

        public async Task<PatientViewModel> Get(Guid id)
        {
            Patient entity = await patientRepository.Get(id);
            List<PatientSite> psListContext = await patientSiteRepository.GetAll();
            List<PatientSite> psList = psListContext.Where(ps => entity.Id == ps.PatientId).ToList();
            List<Site> siteEntitiesContext = await siteRepository.GetAll();
            List<Site> siteEntities = siteEntitiesContext.Where(s => psList.Select(ps => ps.SiteId).Contains(s.Id)).ToList();
            PatientViewModel entityViewModel = new PatientViewModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                SecondName = entity.SecondName,
                Patronymic = entity.Patronymic,
                Address = entity.Address,
                Birthday = entity.Birthday,
                Gender = entity.Gender
            };
            //List<SiteViewModel> lsvm = new List<SiteViewModel>();
            //foreach (var siteEntity in siteEntities)
            //{
            //    lsvm.Add(new SiteViewModel
            //    {
            //        Id = siteEntity.Id,
            //        SiteName = siteEntity.SiteName
            //    });
            //}
            //entityViewModel.SiteList = lsvm;
            entityViewModel.SiteNames = String.Join(", ", siteEntities/*.Where(s => patientSiteEntities.Select(ps => ps.SiteId).Contains(s.Id))*/.Select(s => s.SiteName));
            return entityViewModel;
        }

        public async Task<List<PatientViewModel>> GetSome(List<Guid> ids)
        {
            List<Patient> entities = await patientRepository.GetSome(ids);
            List<PatientSite> psListContext = await patientSiteRepository.GetAll();
            List<PatientSite> psList = psListContext.Where(ps=> entities.Select(p=> p.Id).Contains(ps.PatientId)).ToList();
            List<Site> siteEntitiesContext = await siteRepository.GetAll();
            List<Site> siteEntities = siteEntitiesContext.Where(s=> psList.Select(ps=> ps.SiteId).Contains(s.Id)).ToList();
            List<PatientViewModel> entitiesViewModel = new List<PatientViewModel>();
            foreach (var entity in entities)
            {
                PatientViewModel entityViewModel = new PatientViewModel
                {
                    Id = entity.Id,
                    FirstName = entity.FirstName,
                    SecondName = entity.SecondName,
                    Patronymic = entity.Patronymic,
                    Address = entity.Address,
                    Birthday = entity.Birthday,
                    Gender = entity.Gender
                };
                List<PatientSite> patientSiteEntities = psList.Where(ps => ps.PatientId == entityViewModel.Id).ToList();
                //List<SiteViewModel> lsvm = new List<SiteViewModel>();
                //foreach (var siteEntity in siteEntities.Where(s => patientSiteEntities.Select(ps => ps.SiteId).Contains(s.Id)))
                //{
                //    lsvm.Add(new SiteViewModel
                //    {
                //        Id = siteEntity.Id,
                //        SiteName = siteEntity.SiteName
                //    });
                //}
                //entityViewModel.SiteList = lsvm;
                entityViewModel.SiteNames = String.Join(", ", siteEntities.Where(s => patientSiteEntities.Select(ps => ps.SiteId).Contains(s.Id)).Select(s => s.SiteName));
                entitiesViewModel.Add(entityViewModel);
            }
            return entitiesViewModel;
        }
        public async Task Insert(PatientViewModel entityViewModel)
        {
            Patient entity = new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = entityViewModel.FirstName,
                SecondName = entityViewModel.SecondName,
                Patronymic = entityViewModel.Patronymic,
                Address = entityViewModel.Address,
                Birthday = entityViewModel.Birthday,
                Gender = entityViewModel.Gender
            };
            List<PatientSite> psList = new List<PatientSite>();
            List<Site> siteListCreatedContext = await siteRepository.GetAll();
            List<Site> siteListCreated = siteListCreatedContext.Where(s => entityViewModel.SiteNames.Contains(s.SiteName)).ToList();
            foreach(var s in entityViewModel.SiteNames.Split(", "))
            {
                //string siteName = s.Trim(' ');
                if (siteListCreated.Select(sl=> sl.SiteName).Contains(s))//siteName))
                {
                    psList.Add(new PatientSite { PatientId = entity.Id, SiteId = siteListCreated.SingleOrDefault(sl=> sl.SiteName == /*siteName*/s).Id });
                }
                else
                {
                    Site newSite = new Site
                    {
                        Id = Guid.NewGuid(),
                        SiteName = s//siteName
                    };
                    await siteRepository.Insert(newSite);
                    siteListCreated.Add(newSite);
                    psList.Add(new PatientSite { PatientId = entity.Id, SiteId = newSite.Id });
                }
            }
            await patientRepository.Insert(entity);
            await patientSiteRepository.InsertArray(psList);
        }
        public async Task Update(PatientViewModel entityViewModel)
        {
            Patient entity = await patientRepository.Get(entityViewModel.Id);
            entity.FirstName = entityViewModel.FirstName;
            entity.FirstName = entityViewModel.FirstName;
            entity.SecondName = entityViewModel.SecondName;
            entity.Patronymic = entityViewModel.Patronymic;
            entity.Address = entityViewModel.Address;
            entity.Birthday = entityViewModel.Birthday;
            entity.Gender = entityViewModel.Gender;

            List<PatientSite> psList = await patientSiteRepository.GetById(entity.Id);//Все PatientSites этого пациента
            List <Site> siteListCreatedContext = await siteRepository.GetAll();//Все уже созданные Sites
            List<string> siteNames = entityViewModel.SiteNames.Split(", ").ToList();//Те Site которые относятся к новому Patient
            //List<Site> siteListCreated = siteListCreatedContext.Where(s => siteNames.Contains(s.SiteName)).ToList();//
            List<PatientSite> newPSList = new List<PatientSite>();//Какие Site нужно создать
            foreach(var siteName in siteNames)
            {
                if (!siteListCreatedContext.Select(sl=> sl.SiteName).Contains(siteName))
                {
                    if(!psList.Select(ps=> ps.SiteId).Contains(siteListCreatedContext.SingleOrDefault(s=> s.SiteName == siteName).Id))
                    {
                        newPSList.Add(new PatientSite { PatientId = entity.Id, SiteId = siteListCreatedContext.SingleOrDefault(s => s.SiteName == siteName).Id });
                    }
                }
                else
                {
                    Site newSite = new Site
                    {
                        Id = Guid.NewGuid(),
                        SiteName = siteName
                    };
                    await siteRepository.Insert(newSite);
                    newPSList.Add(new PatientSite { PatientId = entityViewModel.Id, SiteId = newSite.Id});
                }
            }



            //foreach (var s in siteListCreated)
            //{
            //    if (psList.Select(ps => ps.SiteId).Contains(s.Id))
            //    {
            //        psList.Remove(psList.SingleOrDefault(ps => ps.SiteId == s.Id));
            //        siteNames.Remove(s.SiteName);
            //    }
            //    else
            //    {
            //        newPSList.Add(new PatientSite { PatientId = entity.Id, SiteId = s.Id });
            //    }
            //}
            //foreach (var siteName in siteNames)//entityViewModel.SiteNames)
            //{
            //    if (!siteListCreated.Select(sl=> sl.SiteName).Contains(siteName))
            //    {
            //        Site newSite = new Site
            //        {
            //            Id = Guid.NewGuid(),
            //            SiteName = siteName
            //        };
            //        await siteRepository.Insert(newSite);
            //        newPSList.Add(new PatientSite { PatientId = entityViewModel.Id, SiteId = newSite.Id});
            //    }
            //}
            await patientSiteRepository.DeleteArray(psList);
            await patientSiteRepository.InsertArray(newPSList);
            await patientRepository.Update(entity);
        }
        public async Task Delete(Guid id)
        {
            List<PatientSite> psList = await patientSiteRepository.GetAll();
            List<PatientSite> psListToDelete = psList.Where(ps => ps.PatientId == id).ToList();
            await patientSiteRepository.DeleteArray(psList);
            await patientRepository.Delete(id);
        }

        public async Task<bool> IsExist(string name)
        {
            return await patientRepository.IsExist(name);

        }
    }
}
