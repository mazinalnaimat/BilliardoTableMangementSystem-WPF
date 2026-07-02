using BilliardGameTablesManagement.Business.DTOs;

namespace BilliardGameTablesManagement.Business.Interfaces
{
    public interface IAuthService
    {
        LoginResultDto Login(LoginRequest request);
    }
}
