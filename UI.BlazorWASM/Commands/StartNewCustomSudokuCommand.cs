using Core.Data;
using Core.Serializer;
using System.Threading.Tasks;
using UI.BlazorWASM.Providers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class StartNewCustomSudokuCommand : ICommand
    {
        private readonly SudokuProvider _sudokuProvider;
        private readonly IGridSerializer _hodokuGridConverter;
        private readonly IGridProvider _gridProvider;
        private readonly ModalProvider _modalProvider;
        private readonly CellColorProvider _cellColorProvider;
        private readonly GridHistoryProvider _gridHistoryManager;
        private readonly GameTimerProvider _gameTimerProvider;

        public StartNewCustomSudokuCommand(
            SudokuProvider sudokuProvider,
            IGridProvider gridProvider,
            ModalProvider modalProvider,
            CellColorProvider cellColorProvider,
            GridHistoryProvider gridHistoryManager,
            GameTimerProvider gameTimerProvider)
        {
            _sudokuProvider = sudokuProvider;
            _hodokuGridConverter = GridSerializerFactory.Make(GridSerializerName.Hodoku);
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
