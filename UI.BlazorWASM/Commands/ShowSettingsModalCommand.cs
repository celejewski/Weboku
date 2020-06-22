using System;
using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class ShowSettingsModalCommand : ICommand
    {
        private readonly ModalProvider _modalProvider;

        public ShowSettingsModalCommand(ModalProvider modalProvider)
        {
            _modalProvider = modalProvider;
        }

        public Task Execute()
        {
            _modalProvider.SetModalState(Component.Modals.ModalState.Settings);
            return Task.CompletedTask;
        }
    }
}
