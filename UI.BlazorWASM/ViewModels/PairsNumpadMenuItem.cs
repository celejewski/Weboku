using System.Threading.Tasks;
using UI.BlazorWASM.Filters;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ViewModels
{
    public class PairsNumpadMenuItem : BaseNumpadMenuItem, INumpadMenuLabel
    {
        private readonly IFilterProvider _filterProvider;
        private readonly ISudokuProvider _sudokuProvider;

        public PairsNumpadMenuItem(IFilterProvider filterProvider, ISudokuProvider sudokuProvider, NumpadMenuProvider numpadMenuProvider) : base(numpadMenuProvider)
        {
            _filterProvider = filterProvider;
            _sudokuProvider = sudokuProvider;
        }

        public override bool IsDimmed
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

        public override bool IsSelectable => true;

        public string Label => "Pairs";

        public override bool CanExecute => true;

        public override async Task Execute()
        {
            base.Execute();
            _filterProvider.SetFilter(new PairFilter());
        }
    }
}
