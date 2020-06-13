using Core.Generators;
using System.Threading.Tasks;
using UI.BlazorWASM.Component.Modals;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class StartNewGameCommandV2 : ICommand
    {
        private readonly string _difficulty;
        private readonly RESTGridGeneratorV2 _generator;
        private readonly IGridProvider _gridProvider;
        private readonly ModalProvider _modalProvider;
        private readonly CellColorProvider _cellColorProvider;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly GameTimerProvider _gameTimerProvider;

        public StartNewGameCommandV2(
            string difficulty, 
            RESTGridGeneratorV2 generator, 
            IGridProvider gridProvider, 
            ModalProvider modalProvider,
            CellColorProvider cellColorProvider,
            IGridHistoryManager gridHistoryManager,
            GameTimerProvider gameTimerProvider)
        {
            _difficulty = difficulty;
            _generator = generator;
            _gridProvider = gridProvider;
            _modalProvider = modalProvider;
            _cellColorProvider = cellColorProvider;
            _gridHistoryManager = gridHistoryManager;
            _gameTimerProvider = gameTimerProvider;
        }

        public async Task Execute()
        {
            var sudoku = _generator.WithGiven(_difficulty);
            _gridProvider.Grid = await sudoku;
            _modalProvider.SetModalState(ModalState.None);
            _cellColorProvider.ClearAll();
            _gridHistoryManager.ClearUndo();
            _gameTimerProvider.Start();
        }
    }
}
