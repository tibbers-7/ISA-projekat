using System.Collections.Generic;

namespace BloodBankAPI.Services.Questions
{
    public interface IQuestionService
    {
        IEnumerable<Question> GetAll();
        Question GetById(int id);
    }
}
