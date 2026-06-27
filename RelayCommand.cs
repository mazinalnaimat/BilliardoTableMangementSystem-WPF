using System.Windows.Input;

namespace BilliardGameTablesManagement
{
    // A simple reusable RelayCommand (you can place it in a separate file)
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>?_canExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;
        public void Execute(object parameter) => _execute();

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}