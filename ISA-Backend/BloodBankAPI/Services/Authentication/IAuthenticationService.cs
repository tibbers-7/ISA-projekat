using BloodBankAPI.Materials.DTOs;
using System.Drawing;

namespace BloodBankAPI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task RegisterDonor(DonorRegistrationDTO dto);
        Task RegisterStaff(StaffRegistrationDTO dto);
        Task RegisterAdmin(AdminRegistrationDTO dto);
        Task<bool> CheckIfEmailExistsAsync(string email);
        Task<bool> EmailMatchesPasswordAsync(LoginDTO dto);
        Task<AccessTokenDTO> LogInUserAsync(LoginDTO dto);
        Task SendActivationLink(string email);
        Task<bool> ActivateAccount(string email, string token);

    }
}
