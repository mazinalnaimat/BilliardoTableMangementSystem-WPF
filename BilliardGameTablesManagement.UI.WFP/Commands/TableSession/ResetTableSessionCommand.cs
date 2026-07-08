using BilliardGameTablesManagement.Commands;
using BilliardGameTablesManagement.Services.Interfaces;
using BilliardGameTablesManagement.Stores;
using BilliardGameTablesManagement.ViewModels.Controls;

namespace BilliardGameTablesManagement.UI.WFP.Commands.TableSession
{
    public class ResetTableSessionCommand : CommandBase
    {
        private readonly BilliardTableCardViewModel _viewModel;
        private readonly TableSessionStore _tableSessionStore;
        private readonly ITimerService _timerService;

        public ResetTableSessionCommand(
            BilliardTableCardViewModel viewModel,
            TableSessionStore tableSessionStore,
            ITimerService timerService)
        {
            _viewModel = viewModel
                ?? throw new ArgumentNullException(nameof(viewModel));

            _tableSessionStore = tableSessionStore
                ?? throw new ArgumentNullException(nameof(tableSessionStore));

            _timerService = timerService
                ?? throw new ArgumentNullException(nameof(timerService));
        }

        public override void Execute(object? parameter)
        {
            _tableSessionStore.ResetSession(_viewModel.Session);
            _timerService.Reset();
            _viewModel.NotifySessionChanged();
            RaiseCanExecuteChanged();
        }
    }
}
