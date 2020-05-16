using UI.BlazorWASM.Filters;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ViewModels
{
    public class PairsNumpadMenuItem : INumpadMenuLabel
    {
        private readonly IFilterProvider _filterProvider;
        private readonly ISudokuProvider _sudokuProvider;

        public PairsNumpadMenuItem(IFilterProvider filterProvider, ISudokuProvider sudokuProvider)
        {
            _filterProvider = filterProvider;
            _sudokuProvider = sudokuProvider;
        }

        public bool IsDimmed
        {
            get
            {
                for( int y = 0; y < 9; y++ )
                {
                    for( int x = 0; x < 9; x++ )
                    {
                        if (_sudokuProvider.Cells[x,y].Candidates.Count == 2)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        public bool IsSelectable => true;

        public string Label => "Pairs";

        public bool CanExecute => true;

        public void Execute()
        {
            _filterProvider.SetFilter(new PairFilter());
        }
    }
}
