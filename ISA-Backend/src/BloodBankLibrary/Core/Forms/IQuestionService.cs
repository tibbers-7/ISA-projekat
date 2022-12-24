using System.Collections.Generic;

namespace BloodBankLibrary.Core.Forms
{
    public interface IQuestionService
    {
        IEnumerable<Question> GetAll();
        Question GetById(int id);
    }
}
