using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class ClickableActionFactory
    {
        private readonly GridHistoryProvider _gridHistoryManager;
        private readonly CellColorProvider _cellColorProvider;
        private readonly IGridProvider _gridProvider;

        public ClickableActionFactory(GridHistoryProvider gridHistoryManager, CellColorProvider cellColorProvider, IGridProvider gridProvider)
        {
            _gridHistoryManager = gridHistoryManager;
            _cellColorProvider = cellColorProvider;
            _gridProvider = gridProvider;
        }

        public IClickableAction BrushAction()
        {
            return new BrushAction(_cellColorProvider);
        }
        public IClickableAction MarkerAction()
        {
            return new MarkerAction(_gridHistoryManager, _cellColorProvider, _gridProvider);
        }

        public IClickableAction EraserAction()
        {
            return new EraserAction(_gridProvider, _gridHistoryManager);
        }

        public IClickableAction PencilAction()
        {
            return new PencilAction(_gridHistoryManager, _gridProvider);
        }
    }
}
