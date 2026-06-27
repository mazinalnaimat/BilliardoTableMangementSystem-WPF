using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace BilliardGameTablesManagement
{
    /// <summary>
    /// A billiard table control with time tracking and payment calculation.
    /// </summary>
    public partial class BilliardoTableMangement : UserControl, INotifyPropertyChanged
    {
        private readonly DispatcherTimer _timer;
        private DateTime _startTime;
        private TimeSpan _accumulatedTime;
        private bool _isRunning;

        public static readonly DependencyProperty TableNumberProperty =
            DependencyProperty.Register("TableNumber", typeof(int), typeof(BilliardoTableMangement),
                new PropertyMetadata(1, OnTableNumberChanged));

        public static readonly DependencyProperty RatePerHourProperty =
            DependencyProperty.Register("RatePerHour", typeof(decimal), typeof(BilliardoTableMangement),
                new PropertyMetadata(10.0m, OnRateChanged));

        public int TableNumber
        {
            get => (int)GetValue(TableNumberProperty);
            set => SetValue(TableNumberProperty, value);
        } 

        public decimal RatePerHour
        {
            get => (decimal)GetValue(RatePerHourProperty);
            set => SetValue(RatePerHourProperty, value);
        }

        public TimeSpan ElapsedTime => _isRunning
            ? _accumulatedTime + (DateTime.Now - _startTime)
            : _accumulatedTime;

        public decimal CurrentCost => (decimal)ElapsedTime.TotalHours * RatePerHour;

        public bool IsRunning => _isRunning;
        public bool IsNotRunning => !_isRunning;

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand ResetCommand { get; }

        public BilliardoTableMangement()
        {
            InitializeComponent();

            // Set DataContext to itself for internal bindings
            DataContext = this;

            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += Timer_Tick;

            StartCommand = new RelayCommand(StartTimer, () => !_isRunning);
            StopCommand = new RelayCommand(StopTimer, () => _isRunning);
            ResetCommand = new RelayCommand(ResetTimer);
        }

        // ----------- Timer logic -----------
        private void StartTimer()
        {
            if (!_isRunning)
            {
                _startTime = DateTime.Now;
                _isRunning = true;
                _timer.Start();
                UpdateProperties();
            }
        }

        private void StopTimer()
        {
            if (_isRunning)
            {
                _accumulatedTime += DateTime.Now - _startTime;
                _isRunning = false;
                _timer.Stop();
                UpdateProperties();
            }
        }

        private void ResetTimer()
        {
            bool wasRunning = _isRunning;
            _timer.Stop();
            _isRunning = false;
            _accumulatedTime = TimeSpan.Zero;
            if (wasRunning)
                _startTime = DateTime.Now; // just to keep it valid
            UpdateProperties();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the displayed time and cost every second while running
            if (_isRunning)
            {
                OnPropertyChanged(nameof(ElapsedTime));
                OnPropertyChanged(nameof(CurrentCost));
            }
        }

        // Refresh all relevant bindings (buttons + time/cost)
        private void UpdateProperties()
        {
            OnPropertyChanged(nameof(ElapsedTime));
            OnPropertyChanged(nameof(CurrentCost));
            OnPropertyChanged(nameof(IsRunning));
            OnPropertyChanged(nameof(IsNotRunning));
            //CommandManager.InvalidateRequerySuggested(); // enable/disable buttons
        }

        // ----------- Dependency Property changed callbacks -----------
        private static void OnTableNumberChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BilliardoTableMangement uc)
                uc.OnPropertyChanged(nameof(TableNumber));
        }

        private static void OnRateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BilliardoTableMangement uc)
            {
                uc.OnPropertyChanged(nameof(RatePerHour));
                uc.OnPropertyChanged(nameof(CurrentCost));
            }
        }

        // ----------- INotifyPropertyChanged implementation -----------
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}