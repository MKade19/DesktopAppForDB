﻿using BusStation.Common.Models;

namespace BusStation.API.Services.Abstract
{
    public interface IAuthService 
    {
        public Task<AuthData> Login(User user);
        public Task Logout();
        public Task Register(User user);
    }
}
