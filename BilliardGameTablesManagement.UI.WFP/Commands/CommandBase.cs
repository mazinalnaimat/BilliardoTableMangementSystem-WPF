using System.Windows.Input;

namespace BilliardGameTablesManagement.Commands
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        public abstract void Execute(object? parameter);

        protected static void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
