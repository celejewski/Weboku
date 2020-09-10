using Core;
using Core.Data;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class EraserAction : IClickableAction
    {
        private readonly DomainFacade _domainFacade;
        private readonly GridHistoryProvider _gridHistoryManager;

        private void Clear(Position pos)
        {

        }
        public EraserAction(DomainFacade domainFacade, GridHistoryProvider gridHistoryManager)
        {
            _domainFacade = domainFacade;
            _gridHistoryManager = gridHistoryManager;
        }

        public void LeftClickAction(ClickableActionArgs args)
        {
            Clear(args.Pos);
        }

        public void RightClickAction(ClickableActionArgs args)
        {
            Clear(args.Pos);
        }
    }
}
