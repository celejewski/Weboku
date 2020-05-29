using System.Threading.Tasks;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class RestartCommand : ICommand
    {
        private readonly ISudokuProvider _sudokuProvider;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly IGameTimerProvider _gameTimerProvider;
        private readonly ICellColorProvider _cellColorProvider;

        public RestartCommand(
            ISudokuProvider sudokuProvider, 
            IGridHistoryManager gridHistoryManager,
            IGameTimerProvider gameTimerProvider,
            ICellColorProvider cellColorProvider)
        {
            _sudokuProvider = sudokuProvider;
            _gridHistoryManager = gridHistoryManager;
            _gameTimerProvider = gameTimerProvider;
            _cellColorProvider = cellColorProvider;
        }
        public Task Execute()
        {
            _sudokuProvider.Restart();
            _sudokuProvider.ClearAllCandidates();
            _gridHistoryManager.ClearUndo();
            _gameTimerProvider.Start();
            _cellColorProvider.ClearAll();
            return Task.CompletedTask;
        }
    }
}
