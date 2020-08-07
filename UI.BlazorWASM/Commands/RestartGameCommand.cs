using Core.Data;
using System.Threading.Tasks;
using UI.BlazorWASM.Providers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class RestartGameCommand : ICommand
    {
        private readonly IGridProvider _gridProvider;
        private readonly GridHistoryProvider _gridHistoryManager;
        private readonly GameTimerProvider _gameTimerProvider;
        private readonly CellColorProvider _cellColorProvider;

        public RestartGameCommand(
            IGridProvider gridProvider,
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

            foreach( var pos in Position.All )
            {
                if( !_gridProvider.GetIsGiven(pos) )
                {
                    _gridProvider.SetValue(pos, InputValue.None);
                }
            }

            _gridProvider.ClearCandidates();
            _gameTimerProvider.Start();
            _cellColorProvider.ClearAll();
            return Task.CompletedTask;
        }
    }
}
