using Microsoft.AspNetCore.Mvc;
using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Core.Service;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }


        // GET: api/Question
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_questionService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var question = _questionService.GetById(id);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }
    }
}
