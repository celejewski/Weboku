using System;
using System.Threading.Tasks;

namespace Weboku.UserInterface.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action _action;

        public RelayCommand(Action action)
        {
            _action = action;
        }

        public Task Execute()
        {
            _action?.Invoke();

            return Task.CompletedTask;
        }
    }
}