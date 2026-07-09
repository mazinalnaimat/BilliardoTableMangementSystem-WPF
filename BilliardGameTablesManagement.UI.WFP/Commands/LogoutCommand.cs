using BilliardGameTablesManagement.Services.Interfaces;
using BilliardGameTablesManagement.Stores;
using BilliardGameTablesManagement.ViewModels.Windows;

namespace BilliardGameTablesManagement.Commands
{
    public class LogoutCommand : CommandBase
    {
        private readonly BilliardTablesViewModel _viewModel;
        private readonly AuthenticationStore _authenticationStore;
        private readonly INavigationService _navigationService;

        public LogoutCommand(
            BilliardTablesViewModel viewModel,
            AuthenticationStore authenticationStore,
            INavigationService navigationService)
        {
            _viewModel = viewModel
                ?? throw new ArgumentNullException(nameof(viewModel));

            _authenticationStore = authenticationStore
                ?? throw new ArgumentNullException(nameof(authenticationStore));

            _navigationService = navigationService
                ?? throw new ArgumentNullException(nameof(navigationService));
        }

        public override bool CanExecute(object? parameter)
        {
            return _authenticationStore.CurrentUser != null
                || _authenticationStore.CurrentUserInfo != null;
        }

        public override void Execute(object? parameter)
        {
            _authenticationStore.Logout();
            _navigationService.NavigateToLogin(_viewModel);
        }
    }
}
