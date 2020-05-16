using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.Filters;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ViewModels
{
    public class PairsNumpadMenuItem : INumpadMenuLabel
    {
        private readonly IFilterProvider _filterProvider;

        public PairsNumpadMenuItem(IFilterProvider filterProvider)
        {
            _filterProvider = filterProvider;
        }

        public bool IsDimmed => false;

        public bool IsSelectable => true;

        public string Label => "Pairs";

        public bool CanExecute => true;

        public void Execute()
        {
            _filterProvider.SetFilter(new PairFilter());
        }
    }
}
