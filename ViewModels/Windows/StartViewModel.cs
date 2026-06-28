using BilliardGameTablesManagement.Helpers;
using BilliardGameTablesManagement.Services.Interfaces;
using System;
using System.Windows.Input;

namespace BilliardGameTablesManagement.ViewModels.Windows
{
    public class StartViewModel : BaseViewModel
    {
        private readonly IWindowService _windowService;

        public ICommand StartProgramCommand { get; }

        public ICommand ExitCommand { get; }

        public StartViewModel(IWindowService windowService)
        {
            _windowService = windowService
                ?? throw new ArgumentNullException(nameof(windowService));

            StartProgramCommand = new RelayCommand(StartProgram);
            ExitCommand = new RelayCommand(ExitApplication);
        }

        private void StartProgram()
        {
            _windowService.ShowBilliardTablesWindow();
            _windowService.CloseWindowByDataContext(this);
        }

        private void ExitApplication()
        {
            _windowService.Shutdown();
        }
    }
}