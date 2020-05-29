using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class ClickableActionFactory
    {
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly ICellColorProvider _cellColorProvider;
        private readonly ISudokuProvider _sudokuProvider;

        public ClickableActionFactory(IGridHistoryManager gridHistoryManager, ICellColorProvider cellColorProvider, ISudokuProvider sudokuProvider)
        {
            _gridHistoryManager = gridHistoryManager;
            _cellColorProvider = cellColorProvider;
            _sudokuProvider = sudokuProvider;
        }
        
        public IClickableAction ColorAction()
        {
            return new ColorAction(_cellColorProvider); 
        }
        public IClickableAction StandardAction()
        {
            return new StandardAction(_gridHistoryManager, _cellColorProvider, _sudokuProvider);
        }

        public IClickableAction EraseAction()
        {
            return new CleanerAction(_sudokuProvider, _gridHistoryManager);
        }
    }
}
