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

        public CommandProvider(ISudokuGenerator sudokuGenerator, IGridHistoryManager gridHistoryManager, ISudokuProvider sudokuProvider, IGameTimerProvider gameTimerProvider, IGridConverter gridConverter)
        {
            _sudokuGenerator = sudokuGenerator;
            _gridHistoryManager = gridHistoryManager;
            _sudokuProvider = sudokuProvider;
            _gameTimerProvider = gameTimerProvider;
            _gridConverter = gridConverter;
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
    }
}
