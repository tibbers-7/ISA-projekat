

using BloodBankAPI.Model;

namespace BloodBankAPI.Services.Users
{
    public interface IUserService
    {
        Task<IEnumerable<Donor>> GetAllDonors();

        

    }
}
