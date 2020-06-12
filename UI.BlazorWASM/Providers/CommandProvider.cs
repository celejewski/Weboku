using Core.Converters;
using Core.Generators;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Component.NumpadMenu;
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
        private readonly ClickableActionFactory _clickableActionFactory;
        private readonly ModalProvider _modalProvider;
        private readonly NumpadMenuProvider _numpadMenuProvider;
        private readonly PasteProvider _pasteProvider;
        private readonly IGridProvider _gridProvider;
        private readonly RESTGridGeneratorV2 _gridGeneratorV2;

        //private readonly HintsProvider _hintsProvider;

        public CommandProvider(
            ISudokuGenerator sudokuGenerator,
            IGridHistoryManager gridHistoryManager,
            GameTimerProvider gameTimerProvider,
            IGridConverter gridConverter,
            FilterProvider filterProvider,
            CellColorProvider cellColorProvider,
            IClickableActionProvider clickableActionProvider,
            ClickableActionFactory clickableActionFactory,
            ModalProvider modalProvider,
            NumpadMenuProvider numpadMenuProvider,
            PasteProvider pasteProvider,
            IGridProvider gridProvider,
            RESTGridGeneratorV2 gridGeneratorV2)
        {
            _sudokuGenerator = sudokuGenerator;
            _gridHistoryManager = gridHistoryManager;
            _gameTimerProvider = gameTimerProvider;
            _gridConverter = gridConverter;
            _filterProvider = filterProvider;
            _cellColorProvider = cellColorProvider;
            _clickableActionProvider = clickableActionProvider;
            _clickableActionFactory = clickableActionFactory;
            _modalProvider = modalProvider;
            _numpadMenuProvider = numpadMenuProvider;
            _pasteProvider = pasteProvider;
            _gridProvider = gridProvider;
            _gridGeneratorV2 = gridGeneratorV2;
        }
        public ICommand StartNewGame(string difficulty)
        {
            return new StartNewGameCommand(difficulty, _sudokuGenerator, _gridHistoryManager, _gameTimerProvider, _gridConverter, _modalProvider, _cellColorProvider);
        }
        public ICommand SelectValue(int value)
        {
            return new SelectValueCommand(value, _filterProvider, _clickableActionProvider);
        }

        public ICommand StartNewGameV2(string difficulty)
        {
            return new StartNewGameCommandV2(_gridGeneratorV2, _gridProvider, _modalProvider);
        }
    }
}
