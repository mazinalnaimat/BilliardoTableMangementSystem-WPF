using BilliardGameTablesManagement.Services.Interfaces;
using System;
using System.Linq;
using System.Windows;

namespace BilliardGameTablesManagement.Services.Implementations
{
    public class BilliardTablesWindowService : IWindowService
    {
        private readonly Func<Window> _createLoginWindow;
        private readonly Func<Window> _createBilliardTablesWindow;
        private readonly Func<Window> _createUserInfoWindow;
        private Window? _activeUserInfoWindow;

        public BilliardTablesWindowService(
            Func<Window> createLoginWindow,
            Func<Window> createBilliardTablesWindow,
            Func<Window> createUserInfoWindow)
        {
            _createLoginWindow = createLoginWindow
                ?? throw new ArgumentNullException(nameof(createLoginWindow));

            _createBilliardTablesWindow = createBilliardTablesWindow
                ?? throw new ArgumentNullException(nameof(createBilliardTablesWindow));

            _createUserInfoWindow = createUserInfoWindow
                ?? throw new ArgumentNullException(nameof(createUserInfoWindow));
        }

        public void ShowLoginWindow()
        {
            Window loginWindow = _createLoginWindow();
            loginWindow.Show();
        }

        public void ShowBilliardTablesWindow()
        {
            Window billiardTablesWindow = _createBilliardTablesWindow();
            billiardTablesWindow.Show();
        }

        public void ShowUserInfoWindow()
        {
            if (_activeUserInfoWindow != null)
            {
                _activeUserInfoWindow.Activate();
                return;
            }

            _activeUserInfoWindow = _createUserInfoWindow();
            _activeUserInfoWindow.Closed += (_, _) => _activeUserInfoWindow = null;

            Window? owner = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.IsActive);

            if (owner != null && !ReferenceEquals(owner, _activeUserInfoWindow))
            {
                _activeUserInfoWindow.Owner = owner;
            }

            _activeUserInfoWindow.Show();
            _activeUserInfoWindow.Activate();
        }

        public void CloseWindowByDataContext(object dataContext)
        {
            Window? window = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => ReferenceEquals(w.DataContext, dataContext));

            window?.Close();
        }

        public void Shutdown()
        {
            Application.Current.Shutdown();
        }
    }
}
