using BloodBankAPI.Model;
using BloodBankAPI.UnitOfWork;
using System.Collections.Generic;

namespace BloodBankAPI.Services.Forms
{
    public class FormService : IFormService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FormService(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }

        public async Task Create(Form form)
        {
           await _unitOfWork.FormRepository.InsertAsync(form);
        }

        public async Task<IEnumerable<Form>> GetAll()
        {
            return await _unitOfWork.FormRepository.GetAllAsync();
        }

        public async Task<Form> GetByDonorId(int id)
        {
           IEnumerable<Form> forms = await _unitOfWork.FormRepository.GetByConditionAsync(form => form.DonorId == id);
           return forms.FirstOrDefault();
        }

        public async Task<Form> GetById(int id)
        {
            return await _unitOfWork.FormRepository.GetByIdAsync(id);
        }

        public bool IsDonorEligible(Form form)
        {
            return !form.Answers[0];
        }

        public void Update(Form form)
        {
            _unitOfWork.FormRepository.Update(form);
        }
    }
}
