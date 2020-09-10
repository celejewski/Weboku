using Core;
using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class RestartGameCommand : ICommand
    {
        private readonly DomainFacade _gridProvider;
        private readonly GridHistoryProvider _gridHistoryManager;
        private readonly GameTimerProvider _gameTimerProvider;
        private readonly CellColorProvider _cellColorProvider;

        public RestartGameCommand(
            DomainFacade gridProvider,
            GridHistoryProvider gridHistoryManager,
            GameTimerProvider gameTimerProvider,
            CellColorProvider cellColorProvider)
        {
            _gridProvider = gridProvider;
            _gridHistoryManager = gridHistoryManager;
            _gameTimerProvider = gameTimerProvider;
            _cellColorProvider = cellColorProvider;
        }
        public Task Execute()
        {
            _gridHistoryManager.Save();
            _gridProvider.RestartGrid();
            _gameTimerProvider.Start();
            _cellColorProvider.ClearAll();
            return Task.CompletedTask;
        }
    }
}
