namespace BilliardGameTablesManagement.Domain.Entities
{
    public class TableSession
    {
        public int Id { get; set; }
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

        public void Start()
        {
            if (IsRunning)
                return;

            StartTime = DateTime.Now;
            IsRunning = true;
        }

        public void Stop()
        {
            if (!IsRunning)
                return;

            AccumulatedTime += DateTime.Now - StartTime;
            IsRunning = false;
        }

        public void Reset()
        {
            StartTime = DateTime.MinValue;
            AccumulatedTime = TimeSpan.Zero;
            IsRunning = false;
        }
    }
}
