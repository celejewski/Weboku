using Core;
using System.Threading.Tasks;
using UI.BlazorWASM.Component.Modals;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class StartNewGameCommand : ICommand
    {
        private readonly string _difficulty;
        private readonly ModalProvider _modalProvider;
        private readonly CellColorProvider _cellColorProvider;
        private readonly GridHistoryProvider _gridHistoryManager;
        private readonly GameTimerProvider _gameTimerProvider;
        private readonly DomainFacade _domainFacade;

        public StartNewGameCommand(
            string difficulty,
            ModalProvider modalProvider,
            CellColorProvider cellColorProvider,
            GridHistoryProvider gridHistoryManager,
            GameTimerProvider gameTimerProvider,
            DomainFacade domainFacade)
        {
            _difficulty = difficulty;
            _modalProvider = modalProvider;
            _cellColorProvider = cellColorProvider;
            _gridHistoryManager = gridHistoryManager;
            _gameTimerProvider = gameTimerProvider;
            _domainFacade = domainFacade;
        }

        public async Task Execute()
        {
            _domainFacade.StartNewGame(".5.8...167.6.9.........1..28....6...1..9.4..3...1....49..3.........6.4.553...2.9.");
            _modalProvider.SetModalState(ModalState.None);
            _cellColorProvider.ClearAll();
            _gridHistoryManager.ClearUndo();
            _gameTimerProvider.Start();
        }
    }
}
