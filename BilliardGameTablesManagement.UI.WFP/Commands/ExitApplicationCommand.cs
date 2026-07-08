using BilliardGameTablesManagement.Services.Interfaces;

namespace BilliardGameTablesManagement.Commands
{
    public class ExitApplicationCommand : CommandBase
    {
        private readonly INavigationService _navigationService;

        public ExitApplicationCommand(INavigationService navigationService)
        {
            _navigationService = navigationService
                ?? throw new ArgumentNullException(nameof(navigationService));
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Shutdown();
        }
    }
}
