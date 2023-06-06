using BloodBankAPI.Model;
using BloodBankAPI.Repository;
using BloodBankAPI.Settings;

namespace BloodBankAPI.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IGenericRepository<Account> _accountRepository;
        private readonly IGenericRepository<Profile> _profileRepository;
        private readonly IGenericRepository<Question> _questionRepository;
        private readonly IGenericRepository<Form> _formRepository;
        private readonly IGenericRepository<BloodCenter> _bloodCenterRepository;
        private readonly IGenericRepository<CenterAddress> _centerAddressRepository;
        private readonly IGenericRepository<Appointment> _appointmentRepository;
        private readonly BloodBankDbContext _context;

        public UnitOfWork(IGenericRepository<Account> accountRepository, IGenericRepository<Profile> profileRepository,
            IGenericRepository<Question> questionRepository, IGenericRepository<Form> formRepository,
            IGenericRepository<BloodCenter> bloodCenterRepository, IGenericRepository<CenterAddress> centerAddressRepository,
            IGenericRepository<Appointment> appointmentRepository, BloodBankDbContext context) { 
            _accountRepository = accountRepository;
            _profileRepository = profileRepository;
            _questionRepository = questionRepository;
            _formRepository = formRepository;
            _appointmentRepository= appointmentRepository;
            _context = context;
            _bloodCenterRepository= bloodCenterRepository;
            _centerAddressRepository= centerAddressRepository;
        }

        public IGenericRepository<CenterAddress> AddressRepository { get { return _centerAddressRepository; } }

        public IGenericRepository<BloodCenter> CenterRepository { get { return _bloodCenterRepository; } }

        public IGenericRepository<Account> AccountRepository { get { return _accountRepository; } }

        public IGenericRepository<Profile> ProfileRepository { get { return _profileRepository; } }

        public IGenericRepository<Appointment> AppointmentRepository { get { return _appointmentRepository; } }

        public IGenericRepository<Form> FormRepository { get { return _formRepository; } }

        public IGenericRepository<Question> QuestionRepository { get { return _questionRepository; } }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
