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
        private readonly IGameTimerProvider _gameTimerProvider;
        private readonly IGridConverter _gridConverter;
        private readonly IFilterProvider _filterProvider;
        private readonly ICellColorProvider _cellColorProvider;
        private readonly IClickableActionProvider _clickableActionProvider;
        private readonly ClickableActionFactory _clickableActionFactory;
        private readonly ModalProvider _modalProvider;
        private readonly NumpadMenuProvider _numpadMenuProvider;

        public CommandProvider(
            ISudokuGenerator sudokuGenerator,
            IGridHistoryManager gridHistoryManager,
            ISudokuProvider sudokuProvider,
            IGameTimerProvider gameTimerProvider,
            IGridConverter gridConverter,
            IFilterProvider filterProvider,
            ICellColorProvider cellColorProvider,
            IClickableActionProvider clickableActionProvider,
            ClickableActionFactory clickableActionFactory,
            ModalProvider modalProvider,
            NumpadMenuProvider numpadMenuProvider)
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

        public ICommand Restart()
        {
            return new RestartCommand(_sudokuProvider, _gridHistoryManager, _gameTimerProvider, _cellColorProvider);
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
    }
}
