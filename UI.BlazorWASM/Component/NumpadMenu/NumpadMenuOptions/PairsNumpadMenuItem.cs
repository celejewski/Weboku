using Core.Data;
using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class PairsNumpadMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        private readonly IGridProvider _gridProvider;

        public PairsNumpadMenuItem(NumpadMenuProvider numpadMenuProvider, SelectPairsFilterCommand command, IGridProvider gridProvider) 
            : base(command, numpadMenuProvider.FilterContainer)
        {
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
