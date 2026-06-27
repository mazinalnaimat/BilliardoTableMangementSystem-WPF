using System.Windows;
using BilliardGameTablesManagement.Views;

namespace BilliardGameTablesManagement
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var startWindow = new StartWindow();
            startWindow.Show();
        }
    }
}