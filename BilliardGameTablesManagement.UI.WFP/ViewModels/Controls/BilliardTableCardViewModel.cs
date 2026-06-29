using BilliardGameTablesManagement.Business.DTOs;
using BilliardGameTablesManagement.Business.Interfaces;
using BilliardGameTablesManagement.Business.Requests;
using BilliardGameTablesManagement.Helpers;
using BilliardGameTablesManagement.Services.Interfaces;
using System;
using System.Windows.Input;

namespace BilliardGameTablesManagement.ViewModels.Controls
{
    public class BilliardTableCardViewModel : BaseViewModel
    {
        private TableSessionDto _session;
        private readonly ITimerService _timerService;
        private readonly ITableSessionService _tableSessionService;

        public BilliardTableCardViewModel(
            TableSessionDto session,
            ITimerService timerService,
            ITableSessionService tableSessionService)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _timerService = timerService ?? throw new ArgumentNullException(nameof(timerService));
            _tableSessionService = tableSessionService ?? throw new ArgumentNullException(nameof(tableSessionService));

            _timerService.Tick += OnTimerTick;

            StartCommand = new RelayCommand(Start, () => !_session.IsRunning);
            StopCommand = new RelayCommand(Stop, () => _session.IsRunning);
            ResetCommand = new RelayCommand(Reset);
        }

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

        private void Start()
        {
            _session = _tableSessionService.StartSession(new StartSessionRequest
            {
                SessionId = _session.Id,
                TableNumber = _session.TableNumber,
                RatePerHour = _session.RatePerHour
            });

            _timerService.Start(TimeSpan.FromSeconds(1));
            RaiseAllChanged();
        }

        private void Stop()
        {
            _session = _tableSessionService.StopSession(new StopSessionRequest
            {
                SessionId = _session.Id
            });

            _timerService.Stop();
            RaiseAllChanged();
        }

        private void Reset()
        {
            _session = _tableSessionService.ResetSession(new ResetSessionRequest
            {
                SessionId = _session.Id
            });

            _timerService.Stop();
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
