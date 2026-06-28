using BilliardGameTablesManagement.Domain.Entities;
using BilliardGameTablesManagement.Domain.Interfaces;

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
            // TODO: SELECT Id, TableNumber, RatePerHour, StartTime, AccumulatedTime, IsRunning FROM TableSessions
            return _sessions;
        }



        public void Add(TableSession session)
        {
            // TODO: INSERT INTO TableSessions (...columns...) VALUES (...values...)
            if (session.Id == 0)
                session.Id = _sessions.Count == 0 ? 1 : _sessions.Max(s => s.Id) + 1;

            _sessions.Add(session);
        }

        public void Update(TableSession session)
        {
            // TODO: UPDATE TableSessions SET ... WHERE Id = @id
            var existingSession = _sessions.FirstOrDefault(s => s.Id == session.Id);

            if (existingSession == null)
            {
                Add(session);
                return;
            }

        }
    }
}
