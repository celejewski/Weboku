using Core.Converters;
using Core.Data;
using System;
using System.Threading.Tasks;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class StartNewCustomSudokuCommand : ICommand
    {
        private readonly SudokuProvider _sudokuProvider;
        private readonly HodokuGridConverter _hodokuGridConverter;
        private readonly IGridProvider _gridProvider;
        private readonly ModalProvider _modalProvider;
        private readonly CellColorProvider _cellColorProvider;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly GameTimerProvider _gameTimerProvider;

        public StartNewCustomSudokuCommand(
            SudokuProvider sudokuProvider,
            HodokuGridConverter hodokuGridConverter,
            IGridProvider gridProvider,
            ModalProvider modalProvider,
            CellColorProvider cellColorProvider,
            IGridHistoryManager gridHistoryManager,
            GameTimerProvider gameTimerProvider)
        {
            _sudokuProvider = sudokuProvider;
            _hodokuGridConverter = hodokuGridConverter;
            _gridProvider = gridProvider;
            _modalProvider = modalProvider;
            _cellColorProvider = cellColorProvider;
            _gridHistoryManager = gridHistoryManager;
            _gameTimerProvider = gameTimerProvider;
        }

        public Task Execute()
        {
            _sudokuProvider.Sudoku = new Sudoku
            {
                Given = _hodokuGridConverter.Serialize(_gridProvider.Grid),
                Steps = null,
            };
            _gridProvider.Grid = _hodokuGridConverter.Deserialize(_sudokuProvider.Sudoku.Given);
            _sudokuProvider.IsUserCreatingCustomSudoku = false;
            _modalProvider.SetModalState(Component.Modals.ModalState.None);
            _cellColorProvider.ClearAll();
            _gridHistoryManager.ClearUndo();
            _gameTimerProvider.Start();
            return Task.CompletedTask;
        }
    }
}
