using BilliardGameTablesManagement.Business.DTOs;
using BilliardGameTablesManagement.Business.Interfaces;
using BilliardGameTablesManagement.Models;

namespace BilliardGameTablesManagement.Stores
{
    public class AuthenticationStore
    {
        private readonly IAuthService _authService;

        public AuthenticationStore(IAuthService authService)
        {
            _authService = authService
                ?? throw new ArgumentNullException(nameof(authService));
        }

        public LoginResultDto? CurrentUser { get; private set; }

        public LoginResultDto Login(LoginModel login)
        {
            if (login == null)
                throw new ArgumentNullException(nameof(login));

            LoginResultDto result = _authService.Login(new LoginRequest
            {
                Username = login.Username,
                Password = login.Password
            });

            CurrentUser = result.Success ? result : null;

            return result;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
