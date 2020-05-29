using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class PairsNumpadMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        private readonly ISudokuProvider _sudokuProvider;

        public PairsNumpadMenuItem(ISudokuProvider sudokuProvider, NumpadMenuProvider numpadMenuProvider, CommandProvider commandProvider) 
            : base(numpadMenuProvider, commandProvider.SelectPairsFilter())
        {
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
    }
}
