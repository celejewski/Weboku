using Core.Data;
using Core.Generators;
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
        private readonly IEmptyGridGenerator _emptyGridGenerator;
        private readonly SudokuProvider _sudokuProvider;

        public ShowCustomSudokuCommand(
            ModalProvider modalProvider,
            IGridProvider gridProvider,
            IGridHistoryManager gridHistoryManager,
            IEmptyGridGenerator emptyGridGenerator,
            SudokuProvider sudokuProvider)
        {
            _modalProvider = modalProvider;
            _gridProvider = gridProvider;
            _gridHistoryManager = gridHistoryManager;
            _emptyGridGenerator = emptyGridGenerator;
            _sudokuProvider = sudokuProvider;
        }

        public Task Execute()
        {
            _gridHistoryManager.Save();
            _gridProvider.Grid = _emptyGridGenerator.Empty();
            _sudokuProvider.IsUserCreatingCustomSudoku = true;
            _modalProvider.SetModalState(Component.Modals.ModalState.CustomSudoku);
            return Task.CompletedTask;
        }
    }
}
