using BilliardGameTablesManagement.Business.DTOs;
using BilliardGameTablesManagement.Business.Requests;

namespace BilliardGameTablesManagement.Business.Interfaces
{
    public interface ITableSessionService
    {
        IReadOnlyList<TableSessionDto> GetTables();

        TableSessionDto StartSession(StartSessionRequest request);

        TableSessionDto StopSession(StopSessionRequest request);

        TableSessionDto ResetSession(ResetSessionRequest request);
    }
}
