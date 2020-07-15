using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class BrushAction : IClickableAction
    {
        private readonly CellColorProvider _cellColorProvider;

        public BrushAction(CellColorProvider cellColorProvider)
        {
            _cellColorProvider = cellColorProvider;
        }

        public void LeftClickAction(ClickableActionArgs args)
        {
            _cellColorProvider.ToggleColor(args.Pos, args.Color1);
        }

        public void RightClickAction(ClickableActionArgs args)
        {
            _cellColorProvider.ToggleColor(args.Pos, args.Color2);
        }
    }
}
