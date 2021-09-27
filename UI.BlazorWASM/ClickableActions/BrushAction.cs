using Weboku.Core.Data;
using Weboku.UserInterface.Enums;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.ClickableActions
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
            ToggleColor(args.Position, args.Color1);
        }

        public void RightClickAction(ClickableActionArgs args)
        {
            ToggleColor(args.Position, args.Color2);
        }

        private void ToggleColor(Position position, Color color)
        {
            var colorToSet = _cellColorProvider.GetColor(position) == color ? Color.None : color;
            _cellColorProvider.SetColor(position, colorToSet);
        }
    }
}