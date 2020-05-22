using Core.Converters;
using Core.Generators;
using System.Threading.Tasks;
using UI.BlazorWASM.Component.Modals;
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
        private readonly ModalProvider _modalProvider;

        public StartNewGameCommand(
            string difficulty, 
            ISudokuGenerator sudokuGenerator, 
            IGridHistoryManager gridHistoryManager, 
            ISudokuProvider sudokuProvider, 
            IGameTimerProvider gameTimerProvider, 
            IGridConverter gridConverter,
            ModalProvider modalProvider)
        {
            _difficulty = difficulty;
            _sudokuGenerator = sudokuGenerator;
            _gridHistoryManager = gridHistoryManager;
            _sudokuProvider = sudokuProvider;
            _gameTimerProvider = gameTimerProvider;
            _gridConverter = gridConverter;
            _modalProvider = modalProvider;
        }
        public async Task Execute()
        {
            var sudoku = await _sudokuGenerator.Generate(_difficulty);
            _sudokuProvider.Sudoku = sudoku;
            var newGrid = _gridConverter.FromText(sudoku.Given);
            _sudokuProvider.AssignFrom(newGrid);
            _gridHistoryManager.ClearUndo();
            _modalProvider.Modal.SetState(ModalState.EndGame);
            _gameTimerProvider.Start();
        }
    }
}
