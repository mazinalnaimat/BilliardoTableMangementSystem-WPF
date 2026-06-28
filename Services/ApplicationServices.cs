using BilliardGameTablesManagement.Business.Interfaces;
using BilliardGameTablesManagement.Business.Services;
using BilliardGameTablesManagement.DataAccess.Repositories;
using BilliardGameTablesManagement.Domain.Interfaces;

namespace BilliardGameTablesManagement.Services
{
    public static class ApplicationServices
    {
        public static ITableSessionService CreateTableSessionService()
        {
            // WPF is the startup/composition project.
            // It connects the interface from Domain to the implementation from DataAccess.
            ITableSessionRepository repository = new TableSessionRepository();

            return new TableSessionService(repository);
        }
    }
}
