using BilliardGameTablesManagement.Commands;
using BilliardGameTablesManagement.Services.Interfaces;

namespace BilliardGameTablesManagement.UI.WFP.Commands
{
    public class StartProgramCommand : CommandBase
    {
        private readonly INavigationService _navigationService;
        private readonly object _currentDataContext;

        public StartProgramCommand(INavigationService navigationService, object currentDataContext)
        {
            _navigationService = navigationService
                ?? throw new ArgumentNullException(nameof(navigationService));

            _currentDataContext = currentDataContext
                ?? throw new ArgumentNullException(nameof(currentDataContext));
        }

        public override void Execute(object? parameter)
        {
            _navigationService.NavigateToLogin(_currentDataContext);
        }
    }
}
