using BilliardGameTablesManagement.Business.Interfaces;
using BilliardGameTablesManagement.Business.Interfaces.Repos;
using BilliardGameTablesManagement.Business.Interfaces.Serivces;
using BilliardGameTablesManagement.Business.Services;
using BilliardGameTablesManagement.DataAccess.Repositories;
using BilliardGameTablesManagement.Domain.Interfaces;
using BilliardGameTablesManagement.Services.Implementations;
using BilliardGameTablesManagement.Services.Interfaces;
using BilliardGameTablesManagement.Stores;
using BilliardGameTablesManagement.ViewModels.Windows;
using BilliardGameTablesManagement.Views;
using BilliardGameTablesManagement.Views.Windows;

namespace BilliardGameTablesManagement.DependencyInjection
{
    public static class ApplicationServices
    {
        private static ITableSessionService? _tableSessionService;
        private static IAuthService? _authService;
        private static IWindowService? _windowService;
        private static INavigationService? _navigationService;
        private static AuthenticationStore? _authenticationStore;
        private static NavigationStore? _navigationStore;
        private static TableSessionStore? _tableSessionStore;

        public static ITableSessionService TableSessionService =>
            _tableSessionService ??= CreateTableSessionService();

        public static IAuthService AuthService =>
            _authService ??= CreateAuthService();

        public static IWindowService WindowService =>
            _windowService ??= new Services.Implementations.BilliardTablesWindowService(
                CreateLoginWindow,
                CreateBilliardTablesWindow,
                CreateUserInfoWindow);

        public static INavigationService NavigationService =>
            _navigationService ??= new WindowNavigationService(NavigationStore, WindowService);

        public static AuthenticationStore AuthenticationStore =>
            _authenticationStore ??= new AuthenticationStore(AuthService);

        public static NavigationStore NavigationStore =>
            _navigationStore ??= new NavigationStore();

        public static TableSessionStore TableSessionStore =>
            _tableSessionStore ??= new TableSessionStore(TableSessionService);

        public static StartWindow CreateStartWindow()
        {
            NavigationStore.NavigateTo(NavigationRoute.Start);
            return new StartWindow(CreateStartViewModel());
        }

        public static LoginWindow CreateLoginWindow()
        {
            return new LoginWindow(CreateLoginViewModel());
        }

        public static BilliardTablesWindow CreateBilliardTablesWindow()
        {
            return new BilliardTablesWindow(CreateBilliardTablesViewModel());
        }

        public static UserInfoWindow CreateUserInfoWindow()
        {
            return new UserInfoWindow(CreateUserInfoViewModel());
        }

        public static StartViewModel CreateStartViewModel()
        {
            return new StartViewModel(NavigationService);
        }

        public static LoginViewModel CreateLoginViewModel()
        {
            return new LoginViewModel(AuthenticationStore, NavigationService);
        }

        public static BilliardTablesViewModel CreateBilliardTablesViewModel()
        {
            return new BilliardTablesViewModel(
                TableSessionStore,
                AuthenticationStore,
                WindowService,
                () => new DispatcherTimerService());
        }

        public static UserInfoViewModel CreateUserInfoViewModel()
        {
            var userInfo = AuthenticationStore.CurrentUserInfo
                ?? new Models.UserInfoModel { Username = "Unknown" };

            return new UserInfoViewModel(userInfo, WindowService);
        }

        private static ITableSessionService CreateTableSessionService()
        {
            // WPF is the composition root.
            // Here we connect Business interfaces to DataAccess implementations.
            ITableSessionRepository repository = new TableSessionRepository();

            return new TableSessionService(repository);
        }

        private static IAuthService CreateAuthService()
        {
            IUserRepository repository = new UserRepository();

            return new AuthService(repository);
        }
    }
}
