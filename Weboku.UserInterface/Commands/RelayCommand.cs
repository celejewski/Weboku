using System;
using System.Threading.Tasks;

namespace Weboku.UserInterface.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute()
        {
            return _canExecute?.Invoke() ?? true;
        }

        public Task Execute()
        {
            _execute?.Invoke();

            return Task.CompletedTask;
        }
    }
}