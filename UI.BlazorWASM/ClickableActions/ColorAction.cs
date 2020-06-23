using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class ColorAction : IClickableAction
    {
        private readonly CellColorProvider _cellColorProvider;

        public ColorAction(CellColorProvider cellColorProvider)
        {
            _cellColorProvider = cellColorProvider;
        }

        public void LeftClickAction(ClickableActionArgs args)
        {
            _cellColorProvider.ToggleColor(args.X, args.Y, args.Color1);
        }

        public void RightClickAction(ClickableActionArgs args)
        {
            _cellColorProvider.ToggleColor(args.X, args.Y, args.Color2);
        }
    }
}
