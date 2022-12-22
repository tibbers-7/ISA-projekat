using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Core.Repository;
using System.Collections.Generic;

namespace BloodBankLibrary.Core.Service
{


    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }
        public IEnumerable<Question> GetAll()
        {
            return _questionRepository.GetAll();
        }

        public Question GetById(int id)
        {
            return _questionRepository.GetById(id);
        }
    }
}
