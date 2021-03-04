using HRSC.Core.Data;
using HRSC.Core.Extensions;
using HRSC.Core.Managers.Interfaces;
using HRSC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HRSC.Core.Managers
{
    public class UserManager : IUserManager
    {
        private readonly DataContext<User> _userContext;

        public UserManager()
        {
            _userContext = new DataContext<User>();
        }

        public Result<Guid> Create(User user)
        {
            throw new NotImplementedException();
        }

        public User Get(Guid userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public Result<User> Login(string email, string pwd)
        {
            var user = _userContext.GetAll().FirstOrDefault(a => a.EmailAddress.ToLower() == email.ToLower());
            if (user == null)
                return Result<User>.Failure("Email address or password is incorrect");

            if (user.Password != pwd)
                return Result<User>.Failure("Email address or password is incorrect");

            user.LastLogin = DateTime.Now;
            _userContext.Update(user);

            return Result<User>.Success(user, "Login successful");
        }
    }
}
