using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class ColorAction : IClickableAction
    {
        private readonly ICellColorProvider _cellColorProvider;

        public ColorAction(ICellColorProvider cellColorProvider)
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
