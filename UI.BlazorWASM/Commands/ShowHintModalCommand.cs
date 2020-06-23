using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class ShowHintModalCommand : ICommand
    {
        private readonly ModalProvider _modalProvider;
        private readonly HintsProvider _hintsProvider;

        public ShowHintModalCommand(ModalProvider modalProvider, HintsProvider hintsProvider)
        {
            _modalProvider = modalProvider;
            _hintsProvider = hintsProvider;
        }

        public Task Execute()
        {
            _modalProvider.SetModalState(Component.Modals.ModalState.Hints);
            _hintsProvider.SetState(Enums.HintsState.ShowEmpty);
            return Task.CompletedTask;
        }
    }
}
