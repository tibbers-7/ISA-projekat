using BloodBankLibrary.Core.Model;
using System.Collections.Generic;

namespace BloodBankLibrary.Core.Repository
{
    public interface IFormRepository
    {
        IEnumerable<Form> GetAll();
        Form GetById(int id);
        void Create(Form form);
        void Update(Form form);
    }
}
