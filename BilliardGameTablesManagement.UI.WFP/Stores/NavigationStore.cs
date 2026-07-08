namespace BilliardGameTablesManagement.Stores
{
    public class NavigationStore
    {
        private NavigationRoute _currentRoute = NavigationRoute.Start;

        public event Action? CurrentRouteChanged;

        public NavigationRoute CurrentRoute
        {
            get => _currentRoute;
            private set
            {
                if (_currentRoute == value)
                    return;

                _currentRoute = value;
                CurrentRouteChanged?.Invoke();
            }
        }

        public void NavigateTo(NavigationRoute route)
        {
            CurrentRoute = route;
        }
    }
}
