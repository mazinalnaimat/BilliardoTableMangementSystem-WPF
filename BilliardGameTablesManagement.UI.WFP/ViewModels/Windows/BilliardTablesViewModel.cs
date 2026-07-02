using BilliardGameTablesManagement.Business.DTOs;
using BilliardGameTablesManagement.Business.Interfaces;
using BilliardGameTablesManagement.Services.Implementations;
using BilliardGameTablesManagement.ViewModels.Controls;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Threading;

namespace BilliardGameTablesManagement.ViewModels.Windows
{
    public class BilliardTablesViewModel : BaseViewModel
    {
        private readonly ITableSessionService _tableSessionService;
        private readonly DispatcherTimer _clockTimer;

        private string _currentTime;

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

        public BilliardTablesViewModel(ITableSessionService tableSessionService)
        {
            _tableSessionService = tableSessionService
                ?? throw new ArgumentNullException(nameof(tableSessionService));

            Tables = new ObservableCollection<BilliardTableCardViewModel>();

            LoadTables();

            CurrentTime = DateTime.Now.ToString("HH:mm:ss");

            _clockTimer = new DispatcherTimer();
            _clockTimer.Interval = TimeSpan.FromSeconds(1);
            _clockTimer.Tick += ClockTimer_Tick;
            _clockTimer.Start();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToString("HH:mm:ss");
        }

        private void LoadTables()
        {
            IReadOnlyList<TableSessionDto> sessions = _tableSessionService.GetTables();

            Tables.Clear();

            foreach (TableSessionDto session in sessions)
            {
                Tables.Add(new BilliardTableCardViewModel(
                    session,
                    new DispatcherTimerService(),
                    _tableSessionService));
            }
        }
    }
}