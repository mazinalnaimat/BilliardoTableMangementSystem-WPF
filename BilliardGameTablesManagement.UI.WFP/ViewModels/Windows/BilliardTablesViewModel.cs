using BilliardGameTablesManagement.Services.Interfaces;
using BilliardGameTablesManagement.Stores;
using BilliardGameTablesManagement.ViewModels.Controls;
using System;
using System.Collections.ObjectModel;

namespace BilliardGameTablesManagement.ViewModels.Windows
{
    public class BilliardTablesViewModel : BaseViewModel
    {
        private readonly TableSessionStore _tableSessionStore;
        private readonly Func<ITimerService> _timerServiceFactory;
        private readonly ITimerService _clockTimerService;

        private string _currentTime = string.Empty;

        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<BilliardTableCardViewModel> Tables { get; }

        public BilliardTablesViewModel(
            TableSessionStore tableSessionStore,
            Func<ITimerService> timerServiceFactory)
        {
            _tableSessionStore = tableSessionStore
                ?? throw new ArgumentNullException(nameof(tableSessionStore));

            _timerServiceFactory = timerServiceFactory
                ?? throw new ArgumentNullException(nameof(timerServiceFactory));

            Tables = new ObservableCollection<BilliardTableCardViewModel>();

            LoadTables();

            CurrentTime = DateTime.Now.ToString("HH:mm:ss");

            _clockTimerService = _timerServiceFactory();
            _clockTimerService.Tick += ClockTimer_Tick;
            _clockTimerService.Start(TimeSpan.FromSeconds(1));
        }

        private void ClockTimer_Tick(object? sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToString("HH:mm:ss");
        }

        private void LoadTables()
        {
            _tableSessionStore.LoadTables();

            Tables.Clear();

            foreach (var session in _tableSessionStore.Sessions)
            {
                Tables.Add(new BilliardTableCardViewModel(
                    session,
                    _timerServiceFactory(),
                    _tableSessionStore));
            }
        }
    }
}
