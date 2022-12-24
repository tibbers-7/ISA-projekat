using Microsoft.AspNetCore.Mvc;
using BloodBankLibrary.Core.Forms;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;

        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        // GET: api/bloodCenters
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_formService.GetAll());
        }

        // GET api/bloodCenters/2
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var form = _formService.GetById(id);
            if (form == null)
            {
                return NotFound();
            }

            return Ok(form);
        }

        // GET api/bloodCenters/2
        [HttpGet("donor/{id}")]
        public ActionResult IsEligible(int id)
        {
            var form = _formService.GetByDonorId(id);
            if (form == null)
            {
                return NotFound();
            }
            if(!_formService.IsDonorEligible((Form)form)) return NotFound();
            return Ok(form);
        }
        // POST api/bloodCenters
        [HttpPost]
        public ActionResult Create(Form form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _formService.Create(form);
            return CreatedAtAction("GetById", new { id = form.Id }, form);
        }

        // PUT api/bloodCenters/2
        [HttpPut("{id}")]
        public ActionResult Update(int id, Form form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != form.Id)
            {
                return BadRequest();
            }

            try
            {
                _formService.Update(form);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(form);
        }
    }
}
