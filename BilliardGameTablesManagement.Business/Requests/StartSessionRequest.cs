namespace BilliardGameTablesManagement.Business.Requests
{
    public class StartSessionRequest
    {
        public int SessionId { get; set; }
        public int TableNumber { get; set; }
        public decimal RatePerHour { get; set; }
    }
}
