using System;
using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class CloseModalCommand : ICommand
    {
        private readonly ModalProvider _modalProvider;

        public CloseModalCommand(ModalProvider modalProvider)
        {
            _modalProvider = modalProvider;
        }

        public Task Execute()
        {
            _modalProvider.SetModalState(_modalProvider.PreviousState);
            return Task.CompletedTask;
        }
    }
}
