using BilliardGameTablesManagement.Domain.Entities;

namespace BilliardGameTablesManagement.Business.Interfaces
{
    public interface ITableSessionService
    {
        IReadOnlyList<TableSession> GetTables();

        void StartSession(TableSession session);

        void StopSession(TableSession session);

        void ResetSession(TableSession session);
    }
}
