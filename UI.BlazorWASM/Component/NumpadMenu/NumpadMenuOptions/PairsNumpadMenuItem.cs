using Core.Data;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class PairsNumpadMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        private readonly ISudokuProvider _sudokuProvider;
        private readonly IGridProvider _gridProvider;

        public PairsNumpadMenuItem(ISudokuProvider sudokuProvider, NumpadMenuProvider numpadMenuProvider, CommandProvider commandProvider, IGridProvider gridProvider) 
            : base(commandProvider.SelectPairsFilter(), numpadMenuProvider.FilterContainer)
        {
            _sudokuProvider = sudokuProvider;
            _gridProvider = gridProvider;
        }

        public override bool IsDimmed
        {
            get
            {
                for( int y = 0; y < 9; y++ )
                {
                    for( int x = 0; x < 9; x++ )
                    {
                        if (_gridProvider.GetCandidatesCount(x, y) == 2)
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
