using System.Threading.Tasks;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class RestartGameCommand : ICommand
    {
        private readonly ISudokuProvider _sudokuProvider;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly GameTimerProvider _gameTimerProvider;
        private readonly CellColorProvider _cellColorProvider;

        public RestartGameCommand(
            ISudokuProvider sudokuProvider, 
            IGridHistoryManager gridHistoryManager,
            GameTimerProvider gameTimerProvider,
            CellColorProvider cellColorProvider)
        {
            _sudokuProvider = sudokuProvider;
            _gridHistoryManager = gridHistoryManager;
            _gameTimerProvider = gameTimerProvider;
            _cellColorProvider = cellColorProvider;
        }
        public Task Execute()
        {
            _gridHistoryManager.Save();
            _sudokuProvider.RestartGame();
            _gameTimerProvider.Start();
            _cellColorProvider.ClearAll();
            return Task.CompletedTask;
        }
    }
}
