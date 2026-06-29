using BilliardGameTablesManagement.Business.DTOs;
using BilliardGameTablesManagement.Business.Interfaces;
using BilliardGameTablesManagement.Services.Implementations;
using BilliardGameTablesManagement.ViewModels.Controls;
using System.Collections.ObjectModel;

namespace BilliardGameTablesManagement.ViewModels.Windows
{
    public class BilliardTablesViewModel : BaseViewModel
    {
        private readonly ITableSessionService _tableSessionService;

        public ObservableCollection<BilliardTableCardViewModel> Tables { get; }

        public BilliardTablesViewModel(ITableSessionService tableSessionService)
        {
            _tableSessionService = tableSessionService
                ?? throw new ArgumentNullException(nameof(tableSessionService));

            Tables = new ObservableCollection<BilliardTableCardViewModel>();

            LoadTables();
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
