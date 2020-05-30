﻿using System.Threading.Tasks;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class SelectColorMenuItem : ISelectColorMenuItem
    {
        public CellColor Color1 { get; }
        public CellColor Color2 { get; }
        private readonly IClickableActionProvider _clickableActionProvider;
        private readonly NumpadMenuProvider _numpadMenuProvider;

        public SelectColorMenuItem(
            CellColor color1, 
            CellColor color2, 
            IClickableActionProvider clickableActionProvider, 
            NumpadMenuProvider numpadMenuProvider)
        {
            Color1 = color1;
            Color2 = color2;
            _clickableActionProvider = clickableActionProvider;
            _numpadMenuProvider = numpadMenuProvider;
        }

        public bool IsDimmed => false;

        public bool IsSelectable => true;

        public Task Execute()
        {
            _numpadMenuProvider.ColorContainer.SelectItem(this);
            _clickableActionProvider.Color1 = Color1;
            _clickableActionProvider.Color2 = Color2;
            return Task.CompletedTask;
        }
    }
}
