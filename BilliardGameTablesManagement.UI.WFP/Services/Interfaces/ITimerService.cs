namespace BilliardGameTablesManagement.Services.Interfaces
{
    public interface ITimerService
    {
        event EventHandler Tick;
        void Start(TimeSpan interval);
        void Stop();
        void Reset();
        bool IsRunning { get; }
    }
}