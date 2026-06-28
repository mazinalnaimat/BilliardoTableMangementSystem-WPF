using BilliardGameTablesManagement.Business.Interfaces;
using BilliardGameTablesManagement.Domain.Entities;
using BilliardGameTablesManagement.Domain.Interfaces;

namespace BilliardGameTablesManagement.Business.Services
{
    public class TableSessionService : ITableSessionService
    {
        private readonly ITableSessionRepository _tableSessionRepository;

        public TableSessionService(ITableSessionRepository tableSessionRepository)
        {
            _tableSessionRepository = tableSessionRepository
                ?? throw new ArgumentNullException(nameof(tableSessionRepository));
        }

        public IReadOnlyList<TableSession> GetTables()
        {
            return _tableSessionRepository.GetTables();
        }

        public void StartSession(TableSession session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));
         
            session.Start();
            _tableSessionRepository.Update(session);
        }

        public void StopSession(TableSession session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));

            session.Stop();
            _tableSessionRepository.Update(session);
        }

        public void ResetSession(TableSession session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));

            session.Reset();
            _tableSessionRepository.Update(session);
        }
    }
}
