﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu.NumpadMenuOptions
{
    public class SelectColorActionMenuItem : BaseMenuOption, INumpadMenuLabel
    {

        public SelectColorActionMenuItem(NumpadMenuProvider numpadMenuProvider, CommandProvider commandProvider)
            :base(commandProvider.SelectColorAction(), numpadMenuProvider.ActionContainer)
        {

        }
        public string Label => "fas fa-paint-roller";

        public override bool IsDimmed => false;

        public override bool IsSelectable => true;
    }
}
