using Core.Converters;
using Core.Generators;
using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Managers;

namespace UI.BlazorWASM.Providers
{
    public class CommandProvider
    {
        private readonly ISudokuGenerator _sudokuGenerator;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly GameTimerProvider _gameTimerProvider;
        private readonly IGridConverter _gridConverter;
        private readonly FilterProvider _filterProvider;
        private readonly CellColorProvider _cellColorProvider;
        private readonly IClickableActionProvider _clickableActionProvider;
        private readonly ModalProvider _modalProvider;
        private readonly IGridProvider _gridProvider;
        private readonly RESTGridGenerator _gridGeneratorV2;
        private readonly HodokuGridConverter _hodokuGridConverter;
        private readonly SudokuProvider _sudokuProvider;

        //private readonly HintsProvider _hintsProvider;

        public CommandProvider(
            ISudokuGenerator sudokuGenerator,
            IGridHistoryManager gridHistoryManager,
            GameTimerProvider gameTimerProvider,
            IGridConverter gridConverter,
            FilterProvider filterProvider,
            CellColorProvider cellColorProvider,
            IClickableActionProvider clickableActionProvider,
            ModalProvider modalProvider,
            IGridProvider gridProvider,
            RESTGridGenerator gridGeneratorV2,
            HodokuGridConverter hodokuGridConverter,
            SudokuProvider sudokuProvider)
        {
            _sudokuGenerator = sudokuGenerator;
            _gridHistoryManager = gridHistoryManager;
            _gameTimerProvider = gameTimerProvider;
            _gridConverter = gridConverter;
            _filterProvider = filterProvider;
            _cellColorProvider = cellColorProvider;
            _clickableActionProvider = clickableActionProvider;
            _modalProvider = modalProvider;
            _gridProvider = gridProvider;
            _gridGeneratorV2 = gridGeneratorV2;
            _hodokuGridConverter = hodokuGridConverter;
            _sudokuProvider = sudokuProvider;
        }
        public ICommand SelectValue(int value)
        {
            return new SelectValueCommand(value, _filterProvider, _clickableActionProvider);
        }

        public ICommand StartNewGameV2(string difficulty)
        {
            return new StartNewGameCommand(difficulty, _sudokuGenerator, _gridProvider, _modalProvider, _cellColorProvider, _gridHistoryManager, _gameTimerProvider, _hodokuGridConverter, _sudokuProvider);
        }
    }
}
