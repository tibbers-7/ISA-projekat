using System.Collections.Generic;

namespace BloodBankAPI.Services.Forms
{
    public class FormService : IFormService
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

        public object GetByDonorId(int id)
        {
            foreach (Form form in GetAll())
            {
                if (form.DonorId == id) return form;
            }
            return null;
        }

        public Form GetById(int id)
        {
            return _formRepository.GetById(id);
        }

        public bool IsDonorEligible(Form form)
        {
            return !form.Answers[0];
        }

        public void Update(Form form)
        {
            _formRepository.Update(form);
        }
    }
}
