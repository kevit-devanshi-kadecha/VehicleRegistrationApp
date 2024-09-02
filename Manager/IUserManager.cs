﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRegistration.Manager.ManagerModels;

namespace VehicleRegistration.Manager
{
    public interface IUserManager
    {
        Task<UserManagerModel> NewUser(UserManagerModel user);
        Task<(bool isAuthenticated, string message, string jwtToken, DateTime tokenExpiration)> LoginUser(LoginManagerModel loginUser);
    }
}
