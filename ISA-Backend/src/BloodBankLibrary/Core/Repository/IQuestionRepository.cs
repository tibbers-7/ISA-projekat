using BloodBankLibrary.Core.Model;
using System.Collections.Generic;

namespace BloodBankLibrary.Core.Repository
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> GetAll();
        Question GetById(int id);
    }
}
