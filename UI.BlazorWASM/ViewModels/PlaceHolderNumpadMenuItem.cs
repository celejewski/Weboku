using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.BlazorWASM.ViewModels
{
    public class PlaceHolderNumpadMenuItem : INumpadMenuLabel, ISelectColorMenuItem
    {
        public string Label => "";

        public bool IsDimmed => false;

        public bool IsSelectable => false;

        public string CssClass => "";

        public Task Execute() => Task.CompletedTask;
    }
}
