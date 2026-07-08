using BilliardGameTablesManagement.Commands;
using BilliardGameTablesManagement.Services.Interfaces;
using BilliardGameTablesManagement.Stores;
using BilliardGameTablesManagement.ViewModels.Controls;

namespace BilliardGameTablesManagement.UI.WFP.Commands.TableSession
{
    public class StopTableSessionCommand : CommandBase
    {
        private readonly BilliardTableCardViewModel _viewModel;
        private readonly TableSessionStore _tableSessionStore;
        private readonly ITimerService _timerService;

        public StopTableSessionCommand(
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

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.IsRunning;
        }

        public override void Execute(object? parameter)
        {
            _tableSessionStore.StopSession(_viewModel.Session);
            _timerService.Stop();
            _viewModel.NotifySessionChanged();
            RaiseCanExecuteChanged();
        }
    }
}
