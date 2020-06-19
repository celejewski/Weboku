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
            _modalProvider.SetModalState(Component.Modals.ModalState.NewGame);
            return Task.CompletedTask;
        }
    }
}
