using BilliardGameTablesManagement.Commands;
using BilliardGameTablesManagement.Services.Interfaces;
using BilliardGameTablesManagement.Stores;
using BilliardGameTablesManagement.ViewModels.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BilliardGameTablesManagement.ViewModels.Windows
{
    public class BilliardTablesViewModel : BaseViewModel
    {
        private readonly TableSessionStore _tableSessionStore;
        private readonly AuthenticationStore _authenticationStore;
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

        public string CurrentUsername =>
            _authenticationStore.CurrentUserInfo?.Username
            ?? _authenticationStore.CurrentUser?.Username
            ?? "Unknown";

        public ICommand ShowUserInfoCommand { get; }

        public BilliardTablesViewModel(
            TableSessionStore tableSessionStore,
            AuthenticationStore authenticationStore,
            IWindowService windowService,
            Func<ITimerService> timerServiceFactory)
        {
            _tableSessionStore = tableSessionStore
                ?? throw new ArgumentNullException(nameof(tableSessionStore));

            _authenticationStore = authenticationStore
                ?? throw new ArgumentNullException(nameof(authenticationStore));

            if (windowService == null)
                throw new ArgumentNullException(nameof(windowService));

            _timerServiceFactory = timerServiceFactory
                ?? throw new ArgumentNullException(nameof(timerServiceFactory));

            Tables = new ObservableCollection<BilliardTableCardViewModel>();
            ShowUserInfoCommand = new ShowUserInfoCommand(_authenticationStore, windowService);

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
