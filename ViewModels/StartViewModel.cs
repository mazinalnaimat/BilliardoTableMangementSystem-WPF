using System.Windows;
using System.Windows.Input;
using BilliardGameTablesManagement.Helpers;

namespace BilliardGameTablesManagement.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        public ICommand StartProgramCommand { get; }
        public ICommand ExitCommand { get; }

        public StartViewModel()
        {
            StartProgramCommand = new RelayCommand(StartProgram);
            ExitCommand = new RelayCommand(ExitApplication);
        }

        private void StartProgram()
        {
            // Open the main billiard window
            var mainWindow = new MainWindow();
            mainWindow.Show();

            // Close the start screen
            Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.DataContext == this)?
                .Close();
        }

        private void ExitApplication()
        {
            Application.Current.Shutdown();
        }
    }
}