using Core;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class ClickableActionFactory
    {
        private readonly CellColorProvider _cellColorProvider;
        private readonly DomainFacade _domainFacade;

        public ClickableActionFactory(CellColorProvider cellColorProvider, DomainFacade domainFacade)
        {
            _cellColorProvider = cellColorProvider;
            _domainFacade = domainFacade;
        }

        public IClickableAction MakeBrushAction()
        {
            return new BrushAction(_cellColorProvider);
        }
        public IClickableAction MakeMarkerAction()
        {
            return new MarkerAction(_domainFacade);
        }

        public IClickableAction MakeEraserAction()
        {
            return new EraserAction(_domainFacade);
        }

        public IClickableAction MakePencilAction()
        {
            return new PencilAction(_domainFacade);
        }
    }
}
