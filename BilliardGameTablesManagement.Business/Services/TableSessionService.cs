using BilliardGameTablesManagement.Business.DTOs;
using BilliardGameTablesManagement.Business.Interfaces.Repos;
using BilliardGameTablesManagement.Business.Interfaces.Serivces;
using BilliardGameTablesManagement.Business.Mappers;

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

        public IReadOnlyList<TableSessionDto> GetTables()
        {
            return _tableSessionRepository
                .GetTables()
                .Select(TableSessionMapper.ToDto)
                .ToList();
        }

        public TableSessionDto StartSession(StartSessionRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var session = GetRequiredSession(request.SessionId);

            session.TableNumber = request.TableNumber;
            session.RatePerHour = request.RatePerHour;
            session.Start();

            _tableSessionRepository.Update(session);

            return TableSessionMapper.ToDto(session);
        }

        public TableSessionDto StopSession(StopSessionRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var session = GetRequiredSession(request.SessionId);

            session.Stop();
            _tableSessionRepository.Update(session);

            return TableSessionMapper.ToDto(session);
        }

        public TableSessionDto ResetSession(ResetSessionRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var session = GetRequiredSession(request.SessionId);

            session.Reset();
            _tableSessionRepository.Update(session);

            return TableSessionMapper.ToDto(session);
        }

        private Domain.Entities.TableSession GetRequiredSession(int sessionId)
        {
            var session = _tableSessionRepository.GetById(sessionId);

            if (session == null)
                throw new InvalidOperationException($"Table session with Id {sessionId} was not found.");

            return session;
        }
    }
}
