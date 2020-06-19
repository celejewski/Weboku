using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class ShowHowToPlayModalCommand : ICommand
    {
        private readonly ModalProvider _modalProvider;

        public ShowHowToPlayModalCommand(ModalProvider modalProvider)
        {
            _modalProvider = modalProvider;
        }

        public Task Execute()
        {
            _modalProvider.SetModalState(Component.Modals.ModalState.HowToPlay);
            return Task.CompletedTask;
        }
    }
}
