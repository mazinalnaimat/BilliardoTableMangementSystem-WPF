using BilliardGameTablesManagement.Domain.Entities;

namespace BilliardGameTablesManagement.Business.Interfaces.Repos
{
    public interface ITableSessionRepository
    {
        IReadOnlyList<TableSession> GetTables();

        TableSession? GetById(int id);

        void Add(TableSession session);

        void Update(TableSession session);
    }
}
