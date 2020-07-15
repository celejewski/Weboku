using Core.Data;
using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class PairsFilterMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        private readonly IGridProvider _gridProvider;

        public PairsFilterMenuItem(NumpadMenuProvider numpadMenuProvider, SelectPairsFilterCommand command, IGridProvider gridProvider)
            : base(command, numpadMenuProvider.FilterContainer)
        {
            _gridProvider = gridProvider;
        }

        public override bool IsDimmed
        {
            get
            {
                foreach( var pos in Position.All )
                {
                    if( _gridProvider.CandidatesCount(pos) == 2 )
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public override bool IsSelectable => true;

        public string Label => "numpad-pairs__label";
        public override string Tooltip => "numpad-pairs__tooltip";
    }
}
