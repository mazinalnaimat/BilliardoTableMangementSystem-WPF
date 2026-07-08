using BilliardGameTablesManagement.Business.DTOs;

namespace BilliardGameTablesManagement.Models
{
    public class TableSessionModel
    {
        public int Id { get; private set; }

        public int TableNumber { get; set; }

        public decimal RatePerHour { get; set; }

        public DateTime StartTime { get; private set; }

        public TimeSpan AccumulatedTime { get; private set; }

        public bool IsRunning { get; private set; }

        public TimeSpan ElapsedTime =>
            IsRunning
                ? AccumulatedTime + (DateTime.Now - StartTime)
                : AccumulatedTime;

        public decimal CurrentCost => (decimal)ElapsedTime.TotalHours * RatePerHour;

        public static TableSessionModel FromDto(TableSessionDto session)
        {
            var model = new TableSessionModel();
            model.Apply(session);
            return model;
        }

        public void Apply(TableSessionDto session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));

            Id = session.Id;
            TableNumber = session.TableNumber;
            RatePerHour = session.RatePerHour;
            StartTime = session.StartTime;
            AccumulatedTime = session.AccumulatedTime;
            IsRunning = session.IsRunning;
        }
    }
}
