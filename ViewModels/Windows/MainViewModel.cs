using BilliardGameTablesManagement.Business.Interfaces;
using BilliardGameTablesManagement.Domain.Entities;
using BilliardGameTablesManagement.Services.Implementations;
using BilliardGameTablesManagement.ViewModels.Controls;
using System.Collections.ObjectModel;

namespace BilliardGameTablesManagement.ViewModels.Windows
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ITableSessionService _tableSessionService;

        public ObservableCollection<BilliardTableCardViewModel> Tables { get; }

        public MainViewModel(ITableSessionService tableSessionService)
        {
            _tableSessionService = tableSessionService
                ?? throw new ArgumentNullException(nameof(tableSessionService));

            Tables = new ObservableCollection<BilliardTableCardViewModel>();

            LoadTables();
        }

        private void LoadTables()
        {
            IReadOnlyList<TableSession> sessions = _tableSessionService.GetTables();

            Tables.Clear();

            foreach (TableSession session in sessions)
            {
                Tables.Add(new BilliardTableCardViewModel(
                    session,
                    new DispatcherTimerService(),
                    _tableSessionService));
            }
        }
    }
}
