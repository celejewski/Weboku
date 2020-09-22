using Core;

namespace UI.BlazorWASM.ClickableActions
{
    public class EraserAction : IClickableAction
    {
        private readonly DomainFacade _domainFacade;

        public EraserAction(DomainFacade domainFacade)
        {
            _domainFacade = domainFacade;
        }

        public void LeftClickAction(ClickableActionArgs args)
        {
            _domainFacade.UseEraser(args.Position);
        }

        public void RightClickAction(ClickableActionArgs args)
        {
            _domainFacade.UseEraser(args.Position);
        }
    }
}
