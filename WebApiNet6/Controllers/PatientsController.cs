using Microsoft.AspNetCore.Mvc;
using WebApiNet6.Models;
using WebApiNet6.Repo;
using WebApiNet6.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private IPatientRepository _rep;
        public PatientsController(IPatientRepository repository)
        {
            _rep = repository;
        }

        /*private readonly IMapper _mapper;
        private readonly IPatientRepository _rep;
        public PatientsController(IMapper mapper, IPatientRepository repository)
        {
            this._mapper = mapper;
            this._rep = repository;
        }*/

        // GET: api/<PatientsController>
        [HttpGet]
        public IEnumerable<Patient> Get()
        {
            return _rep.FindAll();
        }

        // GET api/<PatientsController>/5
        [HttpGet("{id}")]
        public PatientVm Get(int id)
        {
            return _rep.FindById(id);
        }

        //POST api/<PatientsController>
        [HttpPost]
        public object Post([FromBody]PatientVm entity)
        {
            var isSuccess = _rep.Create(entity);
            return new { Succeeded = isSuccess };
        }

        // PUT api/<PatientsController>/5
        [HttpPut("{id}")]
        public object Put(int id, [FromBody] PatientVm entity)
        {
            var isSuccess = _rep.Update(entity);
            return new { Succeeded = isSuccess };
        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("{id}")]
        public object Delete(int id)
        {
            var isSuccess = _rep.Delete(id);
            return new { Succeeded = isSuccess };
        }

        //[HttpGet]
        [HttpGet("GetMaster")]
        public PatientMasterVm GetMaster()
        {
            //return _rep.GetMaster();
            var master = _rep.GetMaster();
            //var records = _mapper.Map<PatientMasterVm>(master);
            return master;
        }

    }
}
