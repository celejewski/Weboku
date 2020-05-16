using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ViewModels
{
    public class ClearColorsNumpadMenuItem : INumpadMenuItem
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

        public void Execute()
        {
            _cellColorProvider.ClearAll();
        }
    }
}
