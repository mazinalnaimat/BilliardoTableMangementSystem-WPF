using BilliardGameTablesManagement.Domain.Entities;

namespace BilliardGameTablesManagement.Domain.Interfaces
{
    public interface ITableSessionRepository
    {
        IReadOnlyList<TableSession> GetTables();


        void Add(TableSession session);

        void Update(TableSession session);
    }
}
