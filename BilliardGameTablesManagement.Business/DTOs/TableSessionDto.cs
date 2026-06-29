namespace BilliardGameTablesManagement.Business.DTOs
{
    public class TableSessionDto
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public decimal RatePerHour { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan AccumulatedTime { get; set; }
        public bool IsRunning { get; set; }

        public TimeSpan ElapsedTime =>
            IsRunning
                ? AccumulatedTime + (DateTime.Now - StartTime)
                : AccumulatedTime;

        public decimal CurrentCost => (decimal)ElapsedTime.TotalHours * RatePerHour;
    }
}
