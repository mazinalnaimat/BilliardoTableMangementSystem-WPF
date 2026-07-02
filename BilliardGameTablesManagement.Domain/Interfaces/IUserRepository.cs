using BilliardGameTablesManagement.Domain.Entities.User;

namespace BilliardGameTablesManagement.Domain.Interfaces
{
    public interface IUserRepository
    {
        ActiveUser? GetActiveUserByUsername(string Username);
    }
}