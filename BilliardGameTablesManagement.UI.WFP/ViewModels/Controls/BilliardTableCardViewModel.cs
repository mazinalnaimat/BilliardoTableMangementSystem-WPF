using BilliardGameTablesManagement.Commands;
using BilliardGameTablesManagement.Models;
using BilliardGameTablesManagement.Services.Interfaces;
using BilliardGameTablesManagement.Stores;
using System;
using System.Windows.Input;

namespace BilliardGameTablesManagement.ViewModels.Controls
{
    public class BilliardTableCardViewModel : BaseViewModel
    {
        private readonly TableSessionModel _session;
        private readonly ITimerService _timerService;

        public BilliardTableCardViewModel(
            TableSessionModel session,
            ITimerService timerService,
            TableSessionStore tableSessionStore)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _timerService = timerService ?? throw new ArgumentNullException(nameof(timerService));

            if (tableSessionStore == null)
                throw new ArgumentNullException(nameof(tableSessionStore));

            _timerService.Tick += OnTimerTick;

            StartCommand = new StartTableSessionCommand(this, tableSessionStore, _timerService);
            StopCommand = new StopTableSessionCommand(this, tableSessionStore, _timerService);
            ResetCommand = new ResetTableSessionCommand(this, tableSessionStore, _timerService);
        }

        internal TableSessionModel Session => _session;

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

        public ICommand StartCommand { get; }

        public ICommand StopCommand { get; }

        public ICommand ResetCommand { get; }

        private void OnTimerTick(object? sender, EventArgs e)
        {
            if (_session.IsRunning)
            {
                OnPropertyChanged(nameof(ElapsedTime));
                OnPropertyChanged(nameof(CurrentCost));
            }
        }

        internal void NotifySessionChanged()
        {
            RaiseAllChanged();
        }

        private void RaiseAllChanged()
        {
            OnPropertyChanged(nameof(TableNumber));
            OnPropertyChanged(nameof(RatePerHour));
            OnPropertyChanged(nameof(ElapsedTime));
            OnPropertyChanged(nameof(CurrentCost));
            OnPropertyChanged(nameof(IsRunning));
            OnPropertyChanged(nameof(IsNotRunning));
        }
    }
}
