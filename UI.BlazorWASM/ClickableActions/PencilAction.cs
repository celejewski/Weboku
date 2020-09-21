using Core;

namespace UI.BlazorWASM.ClickableActions
{
    public class PencilAction : IClickableAction
    {
        private readonly DomainFacade _domainFacade;

        public PencilAction(DomainFacade domainFacade)
        {
            _domainFacade = domainFacade;
        }

        public void LeftClickAction(ClickableActionArgs args)
        {
            _domainFacade.UsePencil(args.Pos, args.Value);
        }

        public void RightClickAction(ClickableActionArgs args)
        {

            _domainFacade.UseMarker(args.Pos, args.Value);
        }
    }
}
