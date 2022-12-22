using BloodBankLibrary.Core.Model;
using System.Collections.Generic;

namespace BloodBankLibrary.Core.Service
{
    public interface IQuestionService
    {
        IEnumerable<Question> GetAll();
        Question GetById(int id);
    }
}
