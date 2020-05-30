using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class ClearColorsNumpadMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        public ClearColorsNumpadMenuItem(CommandProvider commandProvider)
            :base(commandProvider.ClearColors())
        {
        }
        public override bool IsDimmed => false;

        public override bool IsSelectable => false;

        public string Label => "Clear colors";
    }
}
