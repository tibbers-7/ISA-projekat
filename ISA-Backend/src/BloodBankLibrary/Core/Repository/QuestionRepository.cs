using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly BloodBankDbContext _context;

        public QuestionRepository(BloodBankDbContext context)
        {
            _context = context;
        }
        public void Create(Question question)
        {
            _context.Questions.Add(question);
            _context.SaveChanges();
        }

        public void Delete(Question question)
        {
            _context.Questions.Remove(question);
            _context.SaveChanges();
        }

        public IEnumerable<Question> GetAll()
        {
            return _context.Questions.ToList();
        }

        public Question GetById(int id)
        {
            return _context.Questions.Find(id);
        }

        public void Update(Question question)
        {
            _context.Entry(question).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
