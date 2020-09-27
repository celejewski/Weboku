using Application;
using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class RestartGameCommand : ICommand
    {
        private readonly DomainFacade _domainFacade;
        private readonly CellColorProvider _cellColorProvider;
        private readonly GameTimerProvider _gameTimerProvider;

        public RestartGameCommand(
            DomainFacade domainFacade,
            CellColorProvider cellColorProvider,
            GameTimerProvider gameTimerProvider)
        {
            _domainFacade = domainFacade;
            _cellColorProvider = cellColorProvider;
            _gameTimerProvider = gameTimerProvider;
        }
        public Task Execute()
        {
            _domainFacade.RestartGrid();
            _cellColorProvider.ClearAll();
            _gameTimerProvider.Start();
            return Task.CompletedTask;
        }
    }
}
