using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.IServices
{
    public interface IUserService
    {
        Task CreateNewUser(string email, CreateUpdateUserRequestModel createUpdateUserRequest);
    }
}
