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

        public BilliardTablesWindowService(Func<Window> createLoginWindow, Func<Window> createBilliardTablesWindow)
        {
            _createLoginWindow = createLoginWindow
                ?? throw new ArgumentNullException(nameof(createLoginWindow));

            _createBilliardTablesWindow = createBilliardTablesWindow
                ?? throw new ArgumentNullException(nameof(createBilliardTablesWindow));
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
