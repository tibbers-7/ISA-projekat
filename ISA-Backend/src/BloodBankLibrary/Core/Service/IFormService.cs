﻿using BloodBankLibrary.Core.Model;
using System.Collections.Generic;

namespace BloodBankLibrary.Core.Service
{
    public interface IFormService
    {
        IEnumerable<Form> GetAll();
        Form GetById(int id);
        void Create(Form form);
        void Update(Form form);
    }
}
