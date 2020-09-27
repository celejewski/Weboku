using Application;

namespace UI.BlazorWASM.ClickableActions
{
    public class MarkerAction : IClickableAction
    {
        private readonly DomainFacade _domainFacade;

        public MarkerAction(DomainFacade domainFacade)
        {
            _domainFacade = domainFacade;
        }
        public void LeftClickAction(ClickableActionArgs args)
        {
            _domainFacade.UseMarker(args.Position, args.Value);
        }
        public void RightClickAction(ClickableActionArgs args)
        {
            _domainFacade.UsePencil(args.Position, args.Value);
        }
    }
}
