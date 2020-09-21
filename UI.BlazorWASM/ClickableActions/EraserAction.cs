using Core;
using Core.Data;

namespace UI.BlazorWASM.ClickableActions
{
    public class EraserAction : IClickableAction
    {
        private readonly DomainFacade _domainFacade;

        private void Clear(Position pos)
        {
            _domainFacade.UseEraser(pos);
        }
        public EraserAction(DomainFacade domainFacade)
        {
            _domainFacade = domainFacade;
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
