using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class ColorAction : IClickableAction
    {
        private readonly CellColor _cellColor;
        private readonly ICellColorProvider _cellColorProvider;

        public ColorAction(CellColor cellColor, ICellColorProvider cellColorProvider)
        {
            _cellColor = cellColor;
            _cellColorProvider = cellColorProvider;
        }

        public void LeftClickAction(ClickableActionArgs args)
        {
            _cellColorProvider.ToggleColor(args.X, args.Y, _cellColor);
        }

        public void RightClickAction(ClickableActionArgs args)
        {
            _cellColorProvider.ToggleColor(args.X, args.Y, _cellColor);
        }
    }
}
