using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BloodBankLibrary.Core.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }
        public User GetByEmail(string email)
        {
            foreach(User u in _userRepository.GetAll())
                if (u.Email == email)
                    return u;
             return null;
        }

        public ICollection<User> GetStaffByCenterId(int id)
        {
            List<User> staff = GetAllStaff().ToList();
            staff.RemoveAll(s => s.UserType != "STAFF");
            return staff;

        }

        public void Create(User user)
        {
            _userRepository.Create(user);
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }

        public void Delete(User user)
        {
            _userRepository.Delete(user);
        }

        public ICollection<User> GetAllStaff()
        {
            IEnumerable<User> users = _userRepository.GetAll();
            List<User> staff = new List<User>();
            foreach (User u in users)
            {
                if (u.UserType.Equals("STAFF"))
                {
                    staff.Add(u);
                }
            }

            return staff;

        }
    }
}
