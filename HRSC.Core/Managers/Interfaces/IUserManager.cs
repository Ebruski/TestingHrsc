using HRSC.Core.Extensions;
using HRSC.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRSC.Core.Managers.Interfaces
{
    public interface IUserManager
    {
        User Get(Guid userId);
        IEnumerable<User> GetAll();
        Result<Guid> Create(User user);
        Result<User> Login(string email, string pwd);
    }
}
