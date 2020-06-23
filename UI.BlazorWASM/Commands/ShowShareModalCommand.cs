using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class ShowShareModalCommand : ICommand
    {
        private readonly ModalProvider _modalProvider;

        public ShowShareModalCommand(ModalProvider modalProvider)
        {
            _modalProvider = modalProvider;
        }
        public Task Execute()
        {
            _modalProvider.SetModalState(Component.Modals.ModalState.Share);
            return Task.CompletedTask;
        }
    }
}
