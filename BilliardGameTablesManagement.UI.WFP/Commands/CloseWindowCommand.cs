using BilliardGameTablesManagement.Services.Interfaces;

namespace BilliardGameTablesManagement.Commands
{
    public class CloseWindowCommand : CommandBase
    {
        private readonly IWindowService _windowService;
        private readonly object _dataContext;

        public CloseWindowCommand(IWindowService windowService, object dataContext)
        {
            _windowService = windowService
                ?? throw new ArgumentNullException(nameof(windowService));

            _dataContext = dataContext
                ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public override void Execute(object? parameter)
        {
            _windowService.CloseWindowByDataContext(_dataContext);
        }
    }
}
