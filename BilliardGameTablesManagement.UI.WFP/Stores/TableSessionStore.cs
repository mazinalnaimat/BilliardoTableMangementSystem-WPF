using BilliardGameTablesManagement.Business.DTOs;
using BilliardGameTablesManagement.Business.Interfaces.Serivces;
using BilliardGameTablesManagement.Models;

namespace BilliardGameTablesManagement.Stores
{
    public class TableSessionStore
    {
        private readonly ITableSessionService _tableSessionService;
        private readonly List<TableSessionModel> _sessions = new();

        public TableSessionStore(ITableSessionService tableSessionService)
        {
            _tableSessionService = tableSessionService
                ?? throw new ArgumentNullException(nameof(tableSessionService));
        }

        public IReadOnlyList<TableSessionModel> Sessions => _sessions;

        public event Action? SessionsChanged;

        public event Action<TableSessionModel>? SessionChanged;

        public void LoadTables()
        {
            _sessions.Clear();

            foreach (TableSessionDto session in _tableSessionService.GetTables())
            {
                _sessions.Add(TableSessionModel.FromDto(session));
            }

            SessionsChanged?.Invoke();
        }

        public void StartSession(TableSessionModel session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));

            ApplySessionUpdate(session, _tableSessionService.StartSession(new StartSessionRequest
            {
                SessionId = session.Id,
                TableNumber = session.TableNumber,
                RatePerHour = session.RatePerHour
            }));
        }

        public void StopSession(TableSessionModel session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));

            ApplySessionUpdate(session, _tableSessionService.StopSession(new StopSessionRequest
            {
                SessionId = session.Id
            }));
        }

        public void ResetSession(TableSessionModel session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));

            ApplySessionUpdate(session, _tableSessionService.ResetSession(new ResetSessionRequest
            {
                SessionId = session.Id
            }));
        }

        private void ApplySessionUpdate(TableSessionModel session, TableSessionDto updatedSession)
        {
            session.Apply(updatedSession);
            SessionChanged?.Invoke(session);
        }
    }
}
