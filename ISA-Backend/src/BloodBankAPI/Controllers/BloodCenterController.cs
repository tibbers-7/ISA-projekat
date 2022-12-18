﻿using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Core.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodCenterController : ControllerBase
    {
        private readonly IBloodCenterService _bloodCenterService;

        public BloodCenterController(IBloodCenterService bloodCenterService)
        {
            _bloodCenterService = bloodCenterService;
        }

        // GET: api/bloodCenters
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_bloodCenterService.GetAll());
        }

        // GET api/bloodCenters/2
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var bloodCenter = _bloodCenterService.GetById(id);
            if (bloodCenter == null)
            {
                return NotFound();
            }

            return Ok(bloodCenter);
        }

        // POST api/bloodCenters
        [HttpPost]
        public ActionResult Create(BloodCenter bloodCenter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _bloodCenterService.Create(bloodCenter);
            return CreatedAtAction("GetById", new { id = bloodCenter.Id }, bloodCenter);
        }

        // PUT api/bloodCenters/2
        [HttpPut("{id}")]
        public ActionResult Update(int id, BloodCenter bloodCenter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bloodCenter.Id)
            {
                return BadRequest();
            }

            try
            {
                _bloodCenterService.Update(bloodCenter);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(bloodCenter);
        }

        // DELETE api/bloodCenters/2
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var bloodCenter = _bloodCenterService.GetById(id);
            if (bloodCenter == null)
            {
                return NotFound();
            }

            _bloodCenterService.Delete(bloodCenter);
            return NoContent();
        }

      
    }
}
