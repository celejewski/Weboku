using System.Threading.Tasks;
using UI.BlazorWASM.Component.Modals;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class StartNewGameFromPastedCommand : ICommand
    {
        private readonly PasteProvider _pasteProvider;
        private readonly ModalProvider _modalProvider;
        private readonly CellColorProvider _cellColorProvider;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly GameTimerProvider _gameTimerProvider;
        private readonly IGridProvider _gridProvider;

        public StartNewGameFromPastedCommand(
            PasteProvider pasteProvider, 
            ModalProvider modalProvider,
            CellColorProvider cellColorProvider,
            IGridHistoryManager gridHistoryManager,
            GameTimerProvider gameTimerProvider,
            IGridProvider gridProvider)
        {
            _pasteProvider = pasteProvider;
            _modalProvider = modalProvider;
            _cellColorProvider = cellColorProvider;
            _gridHistoryManager = gridHistoryManager;
            _gameTimerProvider = gameTimerProvider;
            _gridProvider = gridProvider;
        }

        public Task Execute()
        {
            _gridProvider.Grid = _pasteProvider.Grid.Clone();
            _modalProvider.SetModalState(ModalState.None);
            _cellColorProvider.ClearAll();
            _gridHistoryManager.ClearUndo();
            _gameTimerProvider.Start();
            return Task.CompletedTask;
        }
    }
}
