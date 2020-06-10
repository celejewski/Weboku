using System;
using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class ShowHintModalCommand : ICommand
    {
        private readonly ModalProvider _modalProvider;

        public ShowHintModalCommand(ModalProvider modalProvider)
        {
            _modalProvider = modalProvider;
        }

        public Task Execute()
        {
            _modalProvider.SetModalState(Component.Modals.ModalState.Hints);
            return Task.CompletedTask;
        }
    }
}
