using Microsoft.AspNetCore.Mvc;
using Service;
using Service.PatientService;
using Service.SiteService;
using ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/Patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IService<PatientViewModel> patientService;
        private readonly ISiteService siteService;
        public PatientController(IService<PatientViewModel> patientService, ISiteService siteService)
        {
            this.patientService = patientService;
            this.siteService = siteService;
        }

        // GET: api/<PatientController>
        [HttpGet]
        public async Task<List<PatientViewModel>> Get()//List<Guid>? ids)
        {
            List<PatientViewModel> patients = new List<PatientViewModel>();
            patients = /*ids.Count() > 0 ? await patientService.GetSome(ids) : */await patientService.GetAll();
            //patients.ForEach(p => p.SiteNamesSplit = String.Join(", ", p.SiteList.Select(s=> s.SiteName))) ;
            //PatientApiViewModel patients
            return patients;
        }
    
        // GET api/<PatientController>/5
        [HttpGet("{id}")]
        public async Task<PatientViewModel> Get(Guid id)
        {
            PatientViewModel entity = await patientService.Get(id);
            //entity.SiteNamesSplit = String.Join(", ", entity.SiteList.Select(s => s.SiteName)) ;
            return entity;
        }
    
        // POST api/<PatientController>
        [HttpPost]
        public async Task<ActionResult<PatientViewModel>> Post([FromBody] PatientViewModel patient)
        {
            if (patient == null)
            {
                return BadRequest();
            }
            //PatientViewModel entity = new PatientViewModel
            //{
            //    Id = Guid.NewGuid(),
            //    FirstName = patient.FirstName,
            //    SecondName = patient.SecondName,
            //    Patronymic = patient.Patronymic,
            //    Address = patient.Address,
            //    Birthday = patient.Birthday,
            //    Gender = patient.Gender,
            //    //SiteNamesSplit = patient.SiteNamesSplit,
            //    SiteNames = patient.SiteNames//Split.Split(',').ToList()
            //};
            //entity.SiteNames.ForEach(s=> s = s.ToString().Trim());
            await patientService.Insert(patient);
            return Ok(patient);
        }
    
        // PUT api/<PatientController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<PatientViewModel>> Put([FromBody] PatientViewModel patient)
        {
            if (patient == null)
            {
                return BadRequest();
            }
            //PatientViewModel entity = new PatientViewModel
            //{
            //    Id = patient.Id,
            //    FirstName = patient.FirstName,
            //    SecondName = patient.SecondName,
            //    Patronymic = patient.Patronymic,
            //    Address = patient.Address,
            //    Birthday = patient.Birthday,
            //    Gender = patient.Gender,
            //    SiteNamesSplit = patient.SiteNamesSplit,
            //    SiteNames = patient.SiteNamesSplit.Split(',').ToList()
            //};
            //entity.SiteNames.ForEach(s => s = s.ToString());
            await patientService.Update(patient);
            return Ok(patient);
        }

        // DELETE api/<PatientController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await patientService.Delete(id);
            return Ok();
        }
    }
}
