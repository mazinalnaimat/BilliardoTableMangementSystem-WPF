using BilliardGameTablesManagement.Business.DTOs;
using BilliardGameTablesManagement.Services.Interfaces;
using BilliardGameTablesManagement.Stores;
using BilliardGameTablesManagement.ViewModels.Windows;

namespace BilliardGameTablesManagement.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly LoginViewModel _viewModel;
        private readonly AuthenticationStore _authenticationStore;
        private readonly INavigationService _navigationService;

        public LoginCommand(
            LoginViewModel viewModel,
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

        public override void Execute(object? parameter)
        {
            if (!_viewModel.ValidateLoginInput())
                return;

            LoginResultDto result = _authenticationStore.Login(_viewModel.Login);

            if (!result.Success)
            {
                _viewModel.ErrorMessage = result.Message;
                return;
            }

            _navigationService.NavigateToBilliardTables(_viewModel);
        }
    }
}
