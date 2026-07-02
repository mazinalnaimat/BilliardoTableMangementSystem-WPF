using BilliardGameTablesManagement.Business.DTOs;

namespace BilliardGameTablesManagement.Business.Interfaces.Serivces
{
    public interface ITableSessionService
    {
        IReadOnlyList<TableSessionDto> GetTables();

        TableSessionDto StartSession(StartSessionRequest request);

        TableSessionDto StopSession(StopSessionRequest request);

        TableSessionDto ResetSession(ResetSessionRequest request);
    }
}
