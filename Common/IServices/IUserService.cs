﻿using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.IServices
{
    public interface IUserService
    {
        Task<GetByIdUserModel?> GetUserByIdAsync(string email);
        Task CreateNewUser(string email, CreateUpdateUserRequestModel model);
    }
}
