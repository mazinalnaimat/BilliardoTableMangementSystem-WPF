using BilliardGameTablesManagement.Services.Interfaces;
using BilliardGameTablesManagement.Stores;

namespace BilliardGameTablesManagement.Services.Implementations
{
    public class WindowNavigationService : INavigationService
    {
        private readonly NavigationStore _navigationStore;
        private readonly IWindowService _windowService;

        public WindowNavigationService(NavigationStore navigationStore, IWindowService windowService)
        {
            _navigationStore = navigationStore
                ?? throw new ArgumentNullException(nameof(navigationStore));

            _windowService = windowService
                ?? throw new ArgumentNullException(nameof(windowService));
        }

        public void NavigateToLogin(object currentDataContext)
        {
            _navigationStore.NavigateTo(NavigationRoute.Login);
            _windowService.ShowLoginWindow();
            _windowService.CloseWindowByDataContext(currentDataContext);
        }

        public void NavigateToBilliardTables(object currentDataContext)
        {
            _navigationStore.NavigateTo(NavigationRoute.BilliardTables);
            _windowService.ShowBilliardTablesWindow();
            _windowService.CloseWindowByDataContext(currentDataContext);
        }

        public void Shutdown()
        {
            _windowService.Shutdown();
        }
    }
}
