using Core;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class ClickableActionFactory
    {
        private readonly CellColorProvider _cellColorProvider;
        private readonly DomainFacade _gridProvider;

        public ClickableActionFactory(CellColorProvider cellColorProvider, DomainFacade gridProvider)
        {
            _cellColorProvider = cellColorProvider;
            _gridProvider = gridProvider;
        }

        public IClickableAction BrushAction()
        {
            return new BrushAction(_cellColorProvider);
        }
        public IClickableAction MarkerAction()
        {
            return new MarkerAction(_cellColorProvider, _gridProvider);
        }

        public IClickableAction EraserAction()
        {
            return new EraserAction(_gridProvider);
        }

        public IClickableAction PencilAction()
        {
            return new PencilAction(_gridProvider);
        }
    }
}
