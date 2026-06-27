using System;
using System.Windows.Threading;
using BilliardGameTablesManagement.Services.Interfaces;

namespace BilliardGameTablesManagement.Services.Implementations
{
    public class DispatcherTimerService : ITimerService
    {
        private readonly DispatcherTimer _timer = new();

        public event EventHandler? Tick;
        public bool IsRunning => _timer.IsEnabled;

        public void Start(TimeSpan interval)
        {
            _timer.Interval = interval;
            _timer.Tick += OnTimerTick;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            _timer.Tick -= OnTimerTick;
        }

        public void Reset()
        {
            Stop();
            // Optionally notify subscribers of a reset, but not needed here.
        }

        private void OnTimerTick(object? sender, EventArgs e)
        {
            Tick?.Invoke(this, e);
        }
    }
}