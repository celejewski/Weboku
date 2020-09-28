using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class ShowMainMenuModalCommand : ICommand
    {
        private readonly ModalProvider _modalProvider;

        public ShowMainMenuModalCommand(ModalProvider modalProvider)
        {
            _modalProvider = modalProvider;
        }
        public Task Execute()
        {
            _modalProvider.SetModalState(Application.Enums.ModalState.MainMenu);
            return Task.CompletedTask;
        }
    }
}
