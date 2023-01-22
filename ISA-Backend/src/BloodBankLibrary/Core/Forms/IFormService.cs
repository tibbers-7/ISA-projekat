﻿using System.Collections.Generic;

namespace BloodBankLibrary.Core.Forms
{
    public interface IFormService
    {
        IEnumerable<Form> GetAll();
        Form GetById(int id);
        void Create(Form form);
        void Update(Form form);
        bool IsDonorEligible(Form form);
        object GetByDonorId(int id);
    }
}