﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ViewModels
{
    public class ClearColorsNumpadMenuItem : INumpadMenuLabel
    {
        private readonly ICellColorProvider _cellColorProvider;

        public ClearColorsNumpadMenuItem(ICellColorProvider cellColorProvider)
        {
            _cellColorProvider = cellColorProvider;
        }
        public bool IsDimmed => false;

        public bool IsSelectable => false;

        public string Label => "Clear colors";

        public bool CanExecute => true;

        public async Task Execute()
        {
            _cellColorProvider.ClearAll();
        }
    }
}
