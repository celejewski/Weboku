﻿using UI.BlazorWASM.Commands;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class ClearColorsMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        public ClearColorsMenuItem(ClearColorsCommand command)
            : base(command)
        {
        }
        public override bool IsDimmed => false;

        public override bool IsSelectable => false;

        public string Label => "numpad-clear-colors__label";

        public override string Tooltip => "numpad-clear-colors__tooltip";
    }
}