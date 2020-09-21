using Core;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class MarkerAction : IClickableAction
    {
        private readonly CellColorProvider _cellColorProvider;
        private readonly DomainFacade _gridProvider;

        public MarkerAction(
            CellColorProvider cellColorProvider,
            DomainFacade gridProvider
            )
        {
            _cellColorProvider = cellColorProvider;
            _gridProvider = gridProvider;
        }
        public void LeftClickAction(ClickableActionArgs args)
        {
            _gridProvider.UseMarker(args.Pos, args.Value);
        }
        public void RightClickAction(ClickableActionArgs args)
        {
            _gridProvider.UsePencil(args.Pos, args.Value);
        }
    }
}
