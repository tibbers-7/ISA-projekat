using Microsoft.AspNetCore.Mvc;
using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Core.Service;

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

        // POST api/bloodCenters
        [HttpPost]
        public ActionResult Create(Form form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _formService.Create(form);
            return CreatedAtAction("GetById", new { id = form.DonorId }, form);
        }

        // PUT api/bloodCenters/2
        [HttpPut("{id}")]
        public ActionResult Update(int id, Form form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != form.DonorId)
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
