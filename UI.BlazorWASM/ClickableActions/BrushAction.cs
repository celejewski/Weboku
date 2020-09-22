using Core.Data;
using UI.BlazorWASM.Enums;
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
            ToggleColor(args.Pos, args.Color1);
        }

        private void ToggleColor(Position position, Color color)
        {
            var colorToSet = _cellColorProvider.GetColor(position) == color ? Color.None : color;
            _cellColorProvider.SetColor(position, colorToSet);
        }

        public void RightClickAction(ClickableActionArgs args)
        {
            ToggleColor(args.Pos, args.Color2);
        }
    }
}
