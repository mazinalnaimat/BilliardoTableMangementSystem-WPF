using BilliardGameTablesManagement.DependencyInjection;
using System.Windows;

namespace BilliardGameTablesManagement
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var startWindow = ApplicationServices.CreateStartWindow();
            startWindow.Show();
        }
    }
}
