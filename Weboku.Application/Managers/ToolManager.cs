﻿using Weboku.Application.Enums;
using Weboku.Core.Data;

namespace Weboku.Application.Managers
{
    internal sealed class ToolManager
    {
        public void UseMarker(Grid grid, Position position, Value value)
        {
            if (grid.GetIsGiven(position)) return;

            if (!grid.HasValue(position))
            {
                grid.SetValue(position, value);
            }
            else if (grid.GetValue(position) == value)
            {
                grid.SetValue(position, Value.None);
            }
        }

        public void UsePencil(Grid grid, Position position, Value value)
        {
            if (grid.HasValue(position)) return;

            grid.ToggleCandidate(position, value);
        }

        public void UseEraser(Grid grid, Position position)
        {
            if (grid.GetIsGiven(position)) return;

            grid.SetValue(position, Value.None);
            grid.ClearCandidates(position);
        }

        public void UseBrush(ColorManager colorManager, Position position, Color color)
        {
            var colorToSet = colorManager.GetCellColor(position) == color
                ? Color.None
                : color;

            colorManager.SetCellColor(position, colorToSet);
        }
    }
}