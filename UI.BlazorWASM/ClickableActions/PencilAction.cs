using Core;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class PencilAction : IClickableAction
    {
        private readonly GridHistoryProvider _gridHistoryManager;
        private readonly DomainFacade _domainFacade;

        public PencilAction(GridHistoryProvider gridHistoryManager, DomainFacade domainFacade)
        {
            _gridHistoryManager = gridHistoryManager;
            _domainFacade = domainFacade;
        }

        public void LeftClickAction(ClickableActionArgs args)
        {
            _domainFacade.UseMarker(args.Pos, args.Value);
            _gridHistoryManager.Save();
        }

        public void RightClickAction(ClickableActionArgs args)
        {

            _domainFacade.UseMarker(args.Pos, args.Value);
            _gridHistoryManager.Save();
        }
    }
}
