using BilliardGameTablesManagement.Services.Interfaces;
using BilliardGameTablesManagement.ViewModels.Windows;
using System.Linq;
using System.Windows;

namespace BilliardGameTablesManagement.Services.Implementations
{
    public class WindowService : IWindowService
    {
        public void ShowBilliardTablesWindow()
        {
            var tableSessionService = ApplicationServices.CreateTableSessionService();
            var billiardTablesViewModel = new BilliardTablesViewModel(tableSessionService);
            var billiardTablesWindow = new BilliardTablesWindow(billiardTablesViewModel);

            billiardTablesWindow.Show();
        }

        public void CloseWindowByDataContext(object dataContext)
        {
            var window = Application.Current.Windows
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
