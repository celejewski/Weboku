using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class PlaceHolderNumpadMenuItem : INumpadMenuLabel, ISelectColorMenuItem
    {
        public string Label => string.Empty;

        public bool IsDimmed => false;

        public bool IsSelectable => false;

        public CellColor Color1 => CellColor.None;

        public CellColor Color2 => CellColor.None;

        public Task Execute() => Task.CompletedTask;
    }
}
