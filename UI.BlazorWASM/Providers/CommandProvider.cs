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
        private readonly ISudokuProvider _sudokuProvider;
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

        //private readonly HintsProvider _hintsProvider;

        public CommandProvider(
            ISudokuGenerator sudokuGenerator,
            IGridHistoryManager gridHistoryManager,
            ISudokuProvider sudokuProvider,
            GameTimerProvider gameTimerProvider,
            IGridConverter gridConverter,
            FilterProvider filterProvider,
            CellColorProvider cellColorProvider,
            IClickableActionProvider clickableActionProvider,
            ClickableActionFactory clickableActionFactory,
            ModalProvider modalProvider,
            NumpadMenuProvider numpadMenuProvider,
            PasteProvider pasteProvider,
            IGridProvider gridProvider)
        {
            _sudokuGenerator = sudokuGenerator;
            _gridHistoryManager = gridHistoryManager;
            _sudokuProvider = sudokuProvider;
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
        }
        public ICommand StartNewGame(string difficulty)
        {
            return new StartNewGameCommand(difficulty, _sudokuGenerator, _gridHistoryManager, _sudokuProvider, _gameTimerProvider, _gridConverter, _modalProvider, _cellColorProvider);
        }

        public ICommand FindAllCandidates()
        {
            var command = new FindAllCandidatesCommand(_sudokuProvider, _gridHistoryManager);
            return command;
        }

        public ICommand RestartGame()
        {
            return new RestartGameCommand(_sudokuProvider, _gridHistoryManager, _gameTimerProvider, _cellColorProvider);
        }

        public ICommand SelectPairsFilter()
        {
            return new SelectPairsFilterCommand(_filterProvider);
        }

        public ICommand SelectValue(int value)
        {
            return new SelectValueCommand(value, _filterProvider, _clickableActionProvider);
        }

        public ICommand SelectCleaner()
        {
            return new SelectCleanerAction(_clickableActionProvider, _clickableActionFactory, _filterProvider, _numpadMenuProvider);
        }

        public ICommand ClearColors()
        {
            return new ClearColorsCommand(_cellColorProvider);
        }

        public ICommand Redo()
        {
            return new RedoCommand(_gridHistoryManager);
        }

        public ICommand Undo()
        {
            return new UndoCommand(_gridHistoryManager);
        }

        public ICommand ShowNewGameModal()
        {
            return new ShowNewGameModalCommand(_modalProvider);
        }

        public ICommand ShowHowToPlayModal()
        {
            return new ShowHowToPlayModalCommand(_modalProvider);
        }

        public ICommand ShowMainMenuModal()
        {
            return new ShowMainMenuModalCommand(_modalProvider);
        }

        public ICommand CloseModal()
        {
            return new CloseModalCommand(_modalProvider);
        }

        public ICommand SelectStandardAction()
        {
            return new SelectStandardActionCommand(_clickableActionProvider, _clickableActionFactory);
        }

        public ICommand SelectEraserAction()
        {
            return new SelectEraserActionCommand(_clickableActionProvider, _clickableActionFactory);
        }

        public ICommand SelectColorAction()
        {
            return new SelectColorActionCommand(_clickableActionProvider, _clickableActionFactory);
        }

        public ICommand ClearCandidates()
        {
            return new ClearCandidatesCommand(_gridProvider, _gridHistoryManager);
        }

        public ICommand ShowShareModal()
        {
            return new ShowShareModalCommand(_modalProvider);
        }

        public ICommand ShowPasteModal()
        {
            return new ShowPasteModalCommand(_modalProvider);
        }

        public ICommand StartNewGameFromPasted()
        {
            return new StartNewGameFromPastedCommand(
                _sudokuProvider, _pasteProvider, _modalProvider, _cellColorProvider, _gridHistoryManager, _gameTimerProvider);
        }

        public ICommand ShowHintModal()
        {
            return new ShowHintModalCommand(_modalProvider);
        }
    }
}
