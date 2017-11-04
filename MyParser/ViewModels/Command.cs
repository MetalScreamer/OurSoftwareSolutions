using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Oss.Windows.ViewModels
{
    class Command : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public Command(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute ?? (p => true);
        }

        public bool CanExecute(object parameter) => canExecute.Invoke(parameter);
        public void Execute(object parameter) => execute(parameter);

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
