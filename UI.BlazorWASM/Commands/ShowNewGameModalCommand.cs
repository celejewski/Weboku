using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class ShowNewGameModalCommand : ICommand
    {
        private readonly ModalProvider _modalProvider;

        public ShowNewGameModalCommand(ModalProvider modalProvider)
        {
            _modalProvider = modalProvider;
        }
        public Task Execute()
        {
            _modalProvider.Modal.SetState(Component.Modals.ModalState.NewGame);
            return Task.CompletedTask;
        }
    }
}
