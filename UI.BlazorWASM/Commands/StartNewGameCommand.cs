using Core.Serializer;
using System.Threading.Tasks;
using UI.BlazorWASM.Component.Modals;
using UI.BlazorWASM.Providers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class StartNewGameCommand : ICommand
    {
        private readonly string _difficulty;
        private readonly IGridProvider _gridProvider;
        private readonly ModalProvider _modalProvider;
        private readonly CellColorProvider _cellColorProvider;
        private readonly GridHistoryProvider _gridHistoryManager;
        private readonly GameTimerProvider _gameTimerProvider;
        private readonly IGridSerializer _hodokuGridConverter;
        private readonly SudokuProvider _sudokuProvider;

        public StartNewGameCommand(
            string difficulty,
            IGridProvider gridProvider,
            ModalProvider modalProvider,
            CellColorProvider cellColorProvider,
            GridHistoryProvider gridHistoryManager,
            GameTimerProvider gameTimerProvider,
            SudokuProvider sudokuProvider)
        {
            _difficulty = difficulty;
            _gridProvider = gridProvider;
            _modalProvider = modalProvider;
            _cellColorProvider = cellColorProvider;
            _gridHistoryManager = gridHistoryManager;
            _gameTimerProvider = gameTimerProvider;
            _hodokuGridConverter = GridSerializerFactory.Make(GridSerializerName.Hodoku);
            _sudokuProvider = sudokuProvider;
        }

        public async Task Execute()
        {
            var sudoku = await _sudokuProvider.Generate(_difficulty);
            _gridProvider.Grid = _hodokuGridConverter.Deserialize(sudoku.Given);
            _sudokuProvider.PreferredDifficulty = _difficulty;
            _sudokuProvider.Sudoku = sudoku;
            _modalProvider.SetModalState(ModalState.None);
            _cellColorProvider.ClearAll();
            _gridHistoryManager.ClearUndo();
            _gameTimerProvider.Start();
        }
    }
}
