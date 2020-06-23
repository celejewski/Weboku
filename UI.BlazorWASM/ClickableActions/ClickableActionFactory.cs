using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class ClickableActionFactory
    {
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly CellColorProvider _cellColorProvider;
        private readonly IGridProvider _gridProvider;

        public ClickableActionFactory(IGridHistoryManager gridHistoryManager, CellColorProvider cellColorProvider, IGridProvider gridProvider)
        {
            _gridHistoryManager = gridHistoryManager;
            _cellColorProvider = cellColorProvider;
            _gridProvider = gridProvider;
        }

        public IClickableAction ColorAction()
        {
            return new ColorAction(_cellColorProvider);
        }
        public IClickableAction StandardAction()
        {
            return new StandardAction(_gridHistoryManager, _cellColorProvider, _gridProvider);
        }

        public IClickableAction CleanerAction()
        {
            return new CleanerAction(_gridProvider, _gridHistoryManager);
        }

        public IClickableAction EraserAction()
        {
            return new EraserAction(_gridHistoryManager, _gridProvider);
        }
    }
}
