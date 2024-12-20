﻿using ClientMgmtAppAPI.Models.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMgmtAppAPI.Services.IServices
{
    public interface IUserService
    {
        Task<DataResponse<string>> CreateUser(CreateUserDTO request);
        Task<DataResponse<UserInfoDTO>> GetUserInfo();
    }
}
