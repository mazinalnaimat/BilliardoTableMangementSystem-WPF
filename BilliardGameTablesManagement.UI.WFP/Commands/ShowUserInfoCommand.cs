using BilliardGameTablesManagement.Services.Interfaces;
using BilliardGameTablesManagement.Stores;

namespace BilliardGameTablesManagement.Commands
{
    public class ShowUserInfoCommand : CommandBase
    {
        private readonly AuthenticationStore _authenticationStore;
        private readonly IWindowService _windowService;

        public ShowUserInfoCommand(AuthenticationStore authenticationStore, IWindowService windowService)
        {
            _authenticationStore = authenticationStore
                ?? throw new ArgumentNullException(nameof(authenticationStore));

            _windowService = windowService
                ?? throw new ArgumentNullException(nameof(windowService));
        }

        public override bool CanExecute(object? parameter)
        {
            return _authenticationStore.CurrentUserInfo != null;
        }

        public override void Execute(object? parameter)
        {
            _windowService.ShowUserInfoWindow();
        }
    }
}
