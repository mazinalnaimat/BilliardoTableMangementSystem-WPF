using BilliardGameTablesManagement.Business.Interfaces;
using BilliardGameTablesManagement.Business.Services;
using BilliardGameTablesManagement.DataAccess.Repositories;

namespace BilliardGameTablesManagement.Services
{
    public static class ApplicationServices
    {
        public static ITableSessionService CreateTableSessionService()
        {
            // WPF is the startup/composition project.
            // It connects Business interfaces to DataAccess implementations.
            ITableSessionRepository repository = new TableSessionRepository();

            return new TableSessionService(repository);
        }
    }
}
