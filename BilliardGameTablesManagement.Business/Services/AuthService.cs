using BilliardGameTablesManagement.Business.DTOs;
using BilliardGameTablesManagement.Business.Interfaces;
using BilliardGameTablesManagement.Domain.Entities.User;
using BilliardGameTablesManagement.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace BilliardGameTablesManagement.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public LoginResultDto Login(LoginRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.Username))
            {
                return new LoginResultDto
                {
                    Success = false,
                    Message = "Username is required."
                };
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                return new LoginResultDto
                {
                    Success = false,
                    Message = "Password is required."
                };
            }

            var user = _userRepository.GetActiveUserByUsername(request.Username);

            if (user == null)
            {
                return new LoginResultDto
                {
                    Success = false,
                    Message = "Invalid username or password."
                };
            }

            string passwordHash = ComputeSha256Hash(request.Password);

            if (!string.Equals(passwordHash, user.Passowrd, StringComparison.OrdinalIgnoreCase))
            {
                return new LoginResultDto
                {
                    Success = false,
                    Message = "Invalid username or password."
                };
            }

            return new LoginResultDto
            {
                Success = true,
                Message = "Login successful.",
                UserId = user.UserId,
                Username = user.Username,
            };
        }

        private static string ComputeSha256Hash(string value)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(value));

            return Convert.ToHexString(bytes);
        }
    }
}
