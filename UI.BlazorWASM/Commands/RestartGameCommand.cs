using System.Threading.Tasks;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class RestartGameCommand : ICommand
    {
        private readonly IGridProvider _gridProvider;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly GameTimerProvider _gameTimerProvider;
        private readonly CellColorProvider _cellColorProvider;

        public RestartGameCommand(
            IGridProvider gridProvider,
            IGridHistoryManager gridHistoryManager,
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

            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    if( !_gridProvider.GetIsGiven(x, y) )
                    {
                        _gridProvider.SetValue(x, y, Core.Data.InputValue.Empty);
                    }
                }
            }

            _gameTimerProvider.Start();
            _cellColorProvider.ClearAll();
            return Task.CompletedTask;
        }
    }
}
