using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class EraseMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        public EraseMenuItem(NumpadMenuProvider numpadMenuProvider,
            CommandProvider commandProvider)
            : base(numpadMenuProvider, commandProvider.SelectErase())
        {

        }
        public string Label => "X";

        public override bool IsDimmed => false;

        public override bool IsSelectable => true;
    }
}
