using BilliardGameTablesManagement.Commands;
using BilliardGameTablesManagement.Services.Interfaces;
using BilliardGameTablesManagement.Stores;
using BilliardGameTablesManagement.ViewModels.Controls;

namespace BilliardGameTablesManagement.UI.WFP.Commands.TableSession
{
    public class StartTableSessionCommand : CommandBase
    {
        private readonly BilliardTableCardViewModel _viewModel;
        private readonly TableSessionStore _tableSessionStore;
        private readonly ITimerService _timerService;

        public StartTableSessionCommand(
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
            return !_viewModel.IsRunning;
        }

        public override void Execute(object? parameter)
        {
            _tableSessionStore.StartSession(_viewModel.Session);
            _timerService.Start(TimeSpan.FromSeconds(1));
            _viewModel.NotifySessionChanged();
            RaiseCanExecuteChanged();
        }
    }
}
