using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public void LeftClickAction(MouseEventArgs e, int x, int y)
        {
            _cellColorProvider.ToggleColor(x, y, _cellColor);
        }

        public void RightClickAction(MouseEventArgs e, int x, int y)
        {
            _cellColorProvider.ToggleColor(x, y, _cellColor);
        }
    }
}
