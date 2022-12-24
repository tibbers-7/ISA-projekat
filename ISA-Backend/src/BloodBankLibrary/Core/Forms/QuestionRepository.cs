using BloodBankLibrary.Settings;
using System.Collections.Generic;
using System.Linq;

namespace BloodBankLibrary.Core.Forms
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly BloodBankDbContext _context;

        public QuestionRepository(BloodBankDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Question> GetAll()
        {
            return _context.Questions.ToList();
        }

        public Question GetById(int id)
        {
            return _context.Questions.Find(id);
        }

    }
}
