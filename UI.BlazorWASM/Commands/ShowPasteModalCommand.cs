using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class ShowPasteModalCommand : ICommand
    {
        private readonly ModalProvider _modalProvider;

        public ShowPasteModalCommand(ModalProvider modalProvider)
        {
            _modalProvider = modalProvider;
        }
        public Task Execute()
        {
            _modalProvider.SetModalState(Application.Enums.ModalState.Paste);
            return Task.CompletedTask;
        }
    }
}
