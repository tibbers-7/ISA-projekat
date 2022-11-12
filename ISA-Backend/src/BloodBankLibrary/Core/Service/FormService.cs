using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Service
{
    internal class FormService : IFormService
    {
        private readonly IFormRepository _formRepository;
        public FormService(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public void Create(Form form)
        {
            _formRepository.Create(form);
        }

        public IEnumerable<Form> GetAll()
        {
            return _formRepository.GetAll();
        }

        public Form GetById(int id)
        {
            return _formRepository.GetById(id);
        }

        public void Update(Form form)
        {
            _formRepository.Update(form);
        }
    }
}
