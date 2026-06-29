using BilliardGameTablesManagement.Business.DTOs;
using BilliardGameTablesManagement.Domain.Entities;

namespace BilliardGameTablesManagement.Business.Mappers
{
    public static class TableSessionMapper
    {
        public static TableSessionDto ToDto(TableSession session)
        {
            return new TableSessionDto
            {
                Id = session.Id,
                TableNumber = session.TableNumber,
                RatePerHour = session.RatePerHour,
                StartTime = session.StartTime,
                AccumulatedTime = session.AccumulatedTime,
                IsRunning = session.IsRunning
            };
        }
    }
}
