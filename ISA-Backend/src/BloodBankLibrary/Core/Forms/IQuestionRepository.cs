using System.Collections.Generic;

namespace BloodBankLibrary.Core.Forms
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> GetAll();
        Question GetById(int id);
    }
}
