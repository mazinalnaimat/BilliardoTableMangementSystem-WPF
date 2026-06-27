using BilliardGameTablesManagement.Helpers;
using BilliardGameTablesManagement.Models;
using BilliardGameTablesManagement.Services.Interfaces;
using System;
using System.Windows.Input;

namespace BilliardGameTablesManagement.ViewModels
{
    public class BilliardTableViewModel : BaseViewModel
    {
        private readonly ITimerService _timerService;
        private readonly TableSession _session;

        public BilliardTableViewModel(ITimerService timerService)
        {
            _timerService = timerService ?? throw new ArgumentNullException(nameof(timerService));
            _session = new TableSession();

            _timerService.Tick += OnTimerTick;

            StartCommand = new RelayCommand(Start, () => !_session.IsRunning);
            StopCommand = new RelayCommand(Stop, () => _session.IsRunning);
            ResetCommand = new RelayCommand(Reset);
        }

        // ---------- Bound Properties (unchanged) ----------
        public int TableNumber
        {
            get => _session.TableNumber;
            set
            {
                if (_session.TableNumber != value)
                {
                    _session.TableNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal RatePerHour
        {
            get => _session.RatePerHour;
            set
            {
                if (_session.RatePerHour != value)
                {
                    _session.RatePerHour = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CurrentCost));
                }
            }
        }

        public TimeSpan ElapsedTime => _session.ElapsedTime;
        public decimal CurrentCost => _session.CurrentCost;
        public bool IsRunning => _session.IsRunning;
        public bool IsNotRunning => !_session.IsRunning;

        // ---------- Commands ----------
        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand ResetCommand { get; }

        private void Start()
        {
            _session.StartTime = DateTime.Now;
            _session.IsRunning = true;
            _timerService.Start(TimeSpan.FromSeconds(1));
            RaiseAllChanged();
        }

        private void Stop()
        {
            _session.AccumulatedTime += DateTime.Now - _session.StartTime;
            _session.IsRunning = false;
            _timerService.Stop();
            RaiseAllChanged();
        }

        private void Reset()
        {
            bool wasRunning = _session.IsRunning;
            _timerService.Stop();
            _session.IsRunning = false;
            _session.AccumulatedTime = TimeSpan.Zero;
            if (wasRunning)
                _session.StartTime = DateTime.Now;
            RaiseAllChanged();
        }

        private void OnTimerTick(object? sender, EventArgs e)
        {
            if (_session.IsRunning)
            {
                OnPropertyChanged(nameof(ElapsedTime));
                OnPropertyChanged(nameof(CurrentCost));
            }
        }

        private void RaiseAllChanged()
        {
            OnPropertyChanged(nameof(ElapsedTime));
            OnPropertyChanged(nameof(CurrentCost));
            OnPropertyChanged(nameof(IsRunning));
            OnPropertyChanged(nameof(IsNotRunning));
        }
    }
}