using BloodBankAPI.Materials.DTOs;
using System.Drawing;

namespace BloodBankAPI.Services.Authentication
{
    public interface IAuthenticationService
    {
        void Register(DonorRegistrationDTO dto);
    }
}
