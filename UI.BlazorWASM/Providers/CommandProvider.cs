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
        private readonly ISudokuProvider _sudokuProvider;
        private readonly IGameTimerProvider _gameTimerProvider;
        private readonly IGridConverter _gridConverter;
        private readonly IFilterProvider _filterProvider;
        private readonly ICellColorProvider _cellColorProvider;
        private readonly IClickableActionProvider _clickableActionProvider;

        public CommandProvider(ISudokuGenerator sudokuGenerator, IGridHistoryManager gridHistoryManager, ISudokuProvider sudokuProvider, IGameTimerProvider gameTimerProvider, IGridConverter gridConverter, IFilterProvider filterProvider, ICellColorProvider cellColorProvider, IClickableActionProvider clickableActionProvider)
        {
            _sudokuGenerator = sudokuGenerator;
            _gridHistoryManager = gridHistoryManager;
            _sudokuProvider = sudokuProvider;
            _gameTimerProvider = gameTimerProvider;
            _gridConverter = gridConverter;
            _filterProvider = filterProvider;
            _cellColorProvider = cellColorProvider;
            _clickableActionProvider = clickableActionProvider;
        }
        public ICommand StartNewGame(string difficulty)
        {
            return new StartNewGameCommand(difficulty, _sudokuGenerator, _gridHistoryManager, _sudokuProvider, _gameTimerProvider, _gridConverter);
        }

        public ICommand FindAllCandidates()
        {
            var command = new FindAllCandidatesCommand(_sudokuProvider, _gridHistoryManager);
            return command;
        }

        public ICommand Restart()
        {
            return new RestartCommand(_sudokuProvider, _gridHistoryManager);
        }

        public ICommand SelectPairsFilter()
        {
            return new SelectPairsFilter(_filterProvider);
        }

        public ICommand SelectValue(int value)
        {
            return new SelectValue(value, _gridHistoryManager, _cellColorProvider, _sudokuProvider, _filterProvider, _clickableActionProvider);
        }
    }
}
