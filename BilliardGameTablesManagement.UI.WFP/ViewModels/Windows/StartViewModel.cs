using BilliardGameTablesManagement.Commands;
using BilliardGameTablesManagement.Services.Interfaces;
using System;
using System.Windows.Input;

namespace BilliardGameTablesManagement.ViewModels.Windows
{
    public class StartViewModel : BaseViewModel
    {
        public ICommand StartProgramCommand { get; }

        public ICommand ExitCommand { get; }

        public StartViewModel(INavigationService navigationService)
        {
            if (navigationService == null)
                throw new ArgumentNullException(nameof(navigationService));

            StartProgramCommand = new StartProgramCommand(navigationService, this);
            ExitCommand = new ExitApplicationCommand(navigationService);
        }
    }
}
