using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.CabinetService;
using Service.DoctorService;
using Service.SiteService;
using Service.SpecializationService;
using ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IService<DoctorViewModel> doctorService;
        private readonly ICabinetService cabinetService;
        private readonly ISiteService siteService;
        private readonly ISpecializationService specializationService;
        public DoctorController(IService<DoctorViewModel> doctorService,
            ICabinetService cabinetService,
            ISiteService siteService,
            ISpecializationService specializationService)
        {
            this.doctorService = doctorService;
            this.cabinetService = cabinetService;
            this.siteService = siteService;
            this.specializationService = specializationService;
        }

        // GET: api/<DoctorController>
        [HttpGet]
        public async Task<List<DoctorViewModel>> Get(List<Guid> ids)
        {
            List<DoctorViewModel> doctors = new List<DoctorViewModel>();
            doctors = ids.Count() > 0 ? await doctorService.GetSome(ids) : await doctorService.GetAll();
            return  doctors;
        }


        // GET api/<DoctorController>/5
        [HttpGet("{id}")]
        public async Task<DoctorViewModel> Get(Guid id)
        {
            return await doctorService.Get(id);
        }

        // POST api/<DoctorController>
        [HttpPost]
        public async Task<ActionResult<DoctorViewModel>> Post(DoctorViewModel doctor)
        {
            if (doctor == null)
            {
                return BadRequest();
            }
            if (await cabinetService.IsExist(doctor.CabinetNumber))
            {
                CabinetViewModel cabinet = await cabinetService.GetByName(doctor.CabinetNumber);
                doctor.CabinetId = cabinet.Id;
            }
            else
            {
                CabinetViewModel cabinet = new CabinetViewModel();
                cabinet.Id = Guid.NewGuid();
                cabinet.CabinetNumber = doctor.CabinetNumber;
                await cabinetService.Insert(cabinet);
                doctor.CabinetId = cabinet.Id;
            }
            if (await siteService.IsExist(doctor.SiteName))
            {
                SiteViewModel site = await siteService.GetByName(doctor.SiteName);
                doctor.SiteId = site.Id;
            }
            else
            {
                SiteViewModel site = new SiteViewModel();
                site.Id = Guid.NewGuid();
                site.SiteName = doctor.SiteName;
                await siteService.Insert(site);
                doctor.SiteId = site.Id;
            }
            if (await specializationService.IsExist(doctor.SpecializationName))
            {
                SpecializationViewModel specialization = await specializationService.GetByName(doctor.SpecializationName);
                doctor.SpecializationId = specialization.Id;
            }
            else
            {
                SpecializationViewModel specialization = new SpecializationViewModel();
                specialization.Id = Guid.NewGuid();
                specialization.SpecializationName = doctor.SpecializationName;
                await specializationService.Insert(specialization);
                doctor.SpecializationId = specialization.Id;
            }
            await doctorService.Insert(doctor);
            return Ok(doctor);
        }
        

        // PUT api/<DoctorController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorViewModel>> Put(DoctorViewModel doctor)
        {
            if (doctor == null)
            {
                return BadRequest();
            }
            if (await cabinetService.IsExist(doctor.CabinetNumber))
            {
                CabinetViewModel cabinet = await cabinetService.GetByName(doctor.CabinetNumber);
                doctor.CabinetId = cabinet.Id;
            }
            else
            {
                CabinetViewModel cabinet = new CabinetViewModel();
                cabinet.Id = Guid.NewGuid();
                cabinet.CabinetNumber = doctor.CabinetNumber;
                await cabinetService.Insert(cabinet);
                doctor.CabinetId = cabinet.Id;
            }
            if (await siteService.IsExist(doctor.SiteName))
            {
                SiteViewModel site = await siteService.GetByName(doctor.SiteName);
                doctor.SiteId = site.Id;
            }
            else
            {
                SiteViewModel site = new SiteViewModel();
                site.Id = Guid.NewGuid();
                site.SiteName = doctor.SiteName;
                await siteService.Insert(site);
                doctor.SiteId = site.Id;
            }
            if (await specializationService.IsExist(doctor.SpecializationName))
            {
                SpecializationViewModel specialization = await specializationService.GetByName(doctor.SpecializationName);
                doctor.SpecializationId = specialization.Id;
            }
            else
            {
                SpecializationViewModel specialization = new SpecializationViewModel();
                specialization.Id = Guid.NewGuid();
                specialization.SpecializationName = doctor.SpecializationName;
                await specializationService.Insert(specialization);
                doctor.SpecializationId = specialization.Id;
            }
            await doctorService.Insert(doctor);
            return Ok(doctor);
        }

        // DELETE api/<DoctorController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await doctorService.Delete(id);
            return Ok();
        }
    }
}
