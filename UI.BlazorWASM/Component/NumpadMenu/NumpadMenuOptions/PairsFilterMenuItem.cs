using Application;
using Core.Data;
using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class PairsFilterMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        private readonly DomainFacade _gridProvider;

        public PairsFilterMenuItem(NumpadMenuProvider numpadMenuProvider, SelectPairsFilterCommand command, DomainFacade gridProvider)
            : base(command, numpadMenuProvider.FilterContainer)
        {
            _gridProvider = gridProvider;
        }

        public override bool IsDimmed
        {
            get
            {
                foreach( var pos in Position.Positions )
                {
                    if( _gridProvider.GetCandidatesCount(pos) == 2 )
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
