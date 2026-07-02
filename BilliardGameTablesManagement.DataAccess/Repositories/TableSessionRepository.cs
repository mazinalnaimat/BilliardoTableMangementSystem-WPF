using BilliardGameTablesManagement.Business.Interfaces.Repos;
using BilliardGameTablesManagement.Domain.Entities;

namespace BilliardGameTablesManagement.DataAccess.Repositories
{
    public class TableSessionRepository : ITableSessionRepository
    {
        private readonly List<TableSession> _sessions = new()
        {
            new TableSession { Id = 1, TableNumber = 1, RatePerHour = 10.0m },
            new TableSession { Id = 2, TableNumber = 2, RatePerHour = 10.0m },
            new TableSession { Id = 3, TableNumber = 3, RatePerHour = 10.0m },
            new TableSession { Id = 4, TableNumber = 4, RatePerHour = 10.0m },
            new TableSession { Id = 5, TableNumber = 5, RatePerHour = 12.0m },
            new TableSession { Id = 6, TableNumber = 6, RatePerHour = 12.0m }
        };

        public IReadOnlyList<TableSession> GetTables()
        {
            // TODO: Connect with database here.
            // Example later: SELECT Id, TableNumber, RatePerHour, StartTime, AccumulatedTime, IsRunning FROM TableSessions

            // Hardcoded in-memory data for now to preserve the current UI behavior while testing architecture.
            return _sessions;
        }

        public TableSession? GetById(int id)
        {
            // TODO: Connect with database here.
            // Example later: SELECT * FROM TableSessions WHERE Id = @Id

            return _sessions.FirstOrDefault(session => session.Id == id);
        }

        public void Add(TableSession session)
        {
            // TODO: Connect with database here.
            // Example later: INSERT INTO TableSessions (...columns...) VALUES (...values...)

            if (session.Id == 0)
                session.Id = _sessions.Count == 0 ? 1 : _sessions.Max(s => s.Id) + 1;

            _sessions.Add(session);
        }

        public void Update(TableSession session)
        {
            // TODO: Connect with database here.
            // Example later: UPDATE TableSessions SET ... WHERE Id = @Id

            var existingIndex = _sessions.FindIndex(s => s.Id == session.Id);

            if (existingIndex == -1)
            {
                Add(session);
                return;
            }

            // The current in-memory repository returns the same object reference,
            // so the session is already updated before this method is called.
            _sessions[existingIndex] = session;
        }
    }
}
