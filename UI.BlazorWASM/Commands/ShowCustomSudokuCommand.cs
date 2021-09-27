using System.Threading.Tasks;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Commands
{
    public class ShowCustomSudokuCommand : ICommand
    {
        private readonly ModalProvider _modalProvider;

        public ShowCustomSudokuCommand(ModalProvider modalProvider)
        {
            _modalProvider = modalProvider;
        }

        public Task Execute()
        {
            _modalProvider.SetModalState(Application.Enums.ModalState.CustomSudoku);
            return Task.CompletedTask;
        }
    }
}