using System.Threading.Tasks;
using UI.BlazorWASM.Component.Modals;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class StartNewGameFromPastedCommand : ICommand
    {
        private readonly ISudokuProvider _sudokuProvider;
        private readonly PasteProvider _pasteProvider;
        private readonly ModalProvider _modalProvider;
        private readonly ICellColorProvider _cellColorProvider;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly IGameTimerProvider _gameTimerProvider;

        public StartNewGameFromPastedCommand(
            ISudokuProvider sudokuProvider, 
            PasteProvider pasteProvider, 
            ModalProvider modalProvider,
            ICellColorProvider cellColorProvider,
            IGridHistoryManager gridHistoryManager,
            IGameTimerProvider gameTimerProvider)
        {
            _sudokuProvider = sudokuProvider;
            _pasteProvider = pasteProvider;
            _modalProvider = modalProvider;
            _cellColorProvider = cellColorProvider;
            _gridHistoryManager = gridHistoryManager;
            _gameTimerProvider = gameTimerProvider;
        }

        public Task Execute()
        {
            _sudokuProvider.AssignFrom(_pasteProvider.Grid);
            _modalProvider.SetModalState(ModalState.None);
            _cellColorProvider.ClearAll();
            _gridHistoryManager.ClearUndo();
            _gameTimerProvider.Start();
            return Task.CompletedTask;
        }
    }
}
