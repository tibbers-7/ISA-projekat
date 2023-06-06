using BloodBankAPI.Model;
using BloodBankAPI.Repository;

namespace BloodBankAPI.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<CenterAddress> AddressRepository { get; }
        IGenericRepository<BloodCenter> CenterRepository { get; }
        IGenericRepository<Account> AccountRepository { get; }
        IGenericRepository<Profile> ProfileRepository { get; }
        IGenericRepository<Appointment> AppointmentRepository { get; }
        IGenericRepository<Form> FormRepository { get; }
        IGenericRepository<Question> QuestionRepository { get; }

        Task SaveAsync();

    }
}
