using ClientMgmtAppAPI.Common.Constants;
using ClientMgmtAppAPI.Models.DtoModels;
using ClientMgmtAppAPI.Models.Entities;
using ClientMgmtAppAPI.Services.Data;
using ClientMgmtAppAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClientMgmtAppAPI.Services.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UserService> _logger;

        public UserService(DataContext context, IHttpContextAccessor httpContextAccessor, ILogger<UserService> logger)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<DataResponse<string>> CreateUser(CreateUserDTO request)
        {
            DataResponse<string> userResponse = new();
            var user = await _context.Users.AnyAsync(u => u.Email == request.Email);

            PasswordHashGenerator(request.Password!,
                out byte[] passwordHash,
                out byte[] passwordSalt);

            try
            {
                var userData = new User
                {
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    CompanyName = request.CompanyName,
                    CompanyAddress = request.CompanyAddress,
                    IsTwoFactorEnabled = false,
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow
                };

                if (user)
                {
                    userResponse.Status = false;
                    userResponse.StatusMessage = Messages.ErrorMessage.UserAlreadyExists;
                    return userResponse;
                }

                await _context.AddAsync(userData);
                await _context.SaveChangesAsync();

                userResponse.Status = true;
                userResponse.StatusMessage = Messages.SuccessMessage.BaseSuccess;
                return userResponse;
            }
            catch (Exception ex) when (ex is SqlException || ex is DbUpdateException)
            {
                userResponse.Status = false;
                userResponse.StatusMessage = Messages.ErrorMessage.BaseError;
                _logger.LogError($"{ex.GetType().Name}: {ex.Message} ||| {ex.StackTrace}");
            }
            return userResponse;
        }

        public async Task<DataResponse<UserInfoDTO>> GetUserInfo()
        {
            DataResponse<UserInfoDTO> infoResponse = new();

            try
            {
                var userID = Convert.ToInt32(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (userID == 0 || _httpContextAccessor.HttpContext == null)
                {
                    infoResponse.Status = false;
                    infoResponse.StatusMessage = Messages.ErrorMessage.UserNotFound;
                    _logger.LogWarning($"User Context is null, {Messages.ErrorMessage.UserNotFound}");
                    return infoResponse;
                }

                var userData = await _context.Users
                    .Where(u => u.Id == userID)
                    .Select(u => new UserInfoDTO
                    {
                        Email = u.Email,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                    })
                    .FirstOrDefaultAsync();

                infoResponse.Status = true;
                infoResponse.StatusMessage = Messages.ErrorMessage.BaseError;
                infoResponse.Data = userData;
                return infoResponse;
            }
            catch (Exception ex) when (ex is SqlException)
            {
                infoResponse.Status = false;
                infoResponse.StatusMessage = Messages.ErrorMessage.BaseError;
                _logger.LogError($"{ex.GetType().Name}: {ex.Message} ||| {ex.StackTrace}");
            }
            return infoResponse;
        }

        public void PasswordHashGenerator(string password,
            out byte[] passwordHash,
            out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
