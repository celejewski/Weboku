using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class ShowPreviousModalCommand : ICommand
    {
        private readonly ModalProvider _modalProvider;

        public ShowPreviousModalCommand(ModalProvider modalProvider)
        {
            _modalProvider = modalProvider;
        }

        public Task Execute()
        {
            if( _modalProvider.HasPreviousState )
            {
                _modalProvider.GoToPreviousState();
            }
            return Task.CompletedTask;
        }
    }
}
