using Core.Converters;
using Core.Generators;
using System.Threading.Tasks;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class StartNewGameCommand : ICommand
    {
        private readonly string _difficulty;
        private readonly ISudokuGenerator _sudokuGenerator;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly ISudokuProvider _sudokuProvider;
        private readonly IGameTimerProvider _gameTimerProvider;
        private readonly IGridConverter _gridConverter;

        public StartNewGameCommand(string difficulty, ISudokuGenerator sudokuGenerator, IGridHistoryManager gridHistoryManager, ISudokuProvider sudokuProvider, IGameTimerProvider gameTimerProvider, IGridConverter gridConverter)
        {
            _difficulty = difficulty;
            _sudokuGenerator = sudokuGenerator;
            _gridHistoryManager = gridHistoryManager;
            _sudokuProvider = sudokuProvider;
            _gameTimerProvider = gameTimerProvider;
            _gridConverter = gridConverter;
        }
        public async Task Execute()
        {
            var sudoku = await _sudokuGenerator.Generate(_difficulty);
            _sudokuProvider.Sudoku = sudoku;
            var newGrid = _gridConverter.FromText(sudoku.Given);
            _gridHistoryManager.Save();
            _sudokuProvider.AssignFrom(newGrid);
            _gameTimerProvider.Start();
        }
    }
}
