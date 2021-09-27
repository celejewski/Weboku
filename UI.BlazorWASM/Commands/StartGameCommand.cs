using System.Threading.Tasks;
using Weboku.Application.Enums;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Commands
{
    public class StartGameCommand
    {
        private readonly ModalProvider _modalProvider;
        private readonly CellColorProvider _cellColorProvider;
        private readonly GameTimerProvider _gameTimerProvider;

        public StartGameCommand(
            ModalProvider modalProvider,
            CellColorProvider cellColorProvider,
            GameTimerProvider gameTimerProvider)
        {
            _modalProvider = modalProvider;
            _cellColorProvider = cellColorProvider;
            _gameTimerProvider = gameTimerProvider;
        }

        public Task Execute()
        {
            _modalProvider.SetModalState(ModalState.None);
            _cellColorProvider.ClearAll();
            _gameTimerProvider.Start();
            return Task.CompletedTask;
        }
    }
}