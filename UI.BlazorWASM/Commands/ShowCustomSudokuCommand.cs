using Core.Factories;
using System.Threading.Tasks;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class ShowCustomSudokuCommand : ICommand
    {
        private readonly ModalProvider _modalProvider;
        private readonly IGridProvider _gridProvider;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly SudokuProvider _sudokuProvider;

        public ShowCustomSudokuCommand(
            ModalProvider modalProvider,
            IGridProvider gridProvider,
            IGridHistoryManager gridHistoryManager,
            SudokuProvider sudokuProvider)
        {
            _modalProvider = modalProvider;
            _gridProvider = gridProvider;
            _gridHistoryManager = gridHistoryManager;
            _sudokuProvider = sudokuProvider;
        }

        public Task Execute()
        {
            _gridHistoryManager.Save();
            _gridProvider.Grid = GridFactory.MakeEmpty();
            _sudokuProvider.IsUserCreatingCustomSudoku = true;
            _modalProvider.SetModalState(Component.Modals.ModalState.CustomSudoku);
            return Task.CompletedTask;
        }
    }
}
