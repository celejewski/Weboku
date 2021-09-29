using Weboku.Application;
using Weboku.Core.Data;
using Weboku.UserInterface.Commands;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public class PairsFilterMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        private readonly DomainFacade _gridProvider;

        public PairsFilterMenuItem(NumpadMenuProvider numpadMenuProvider, ICommand command, DomainFacade gridProvider)
            : base(command, numpadMenuProvider.FilterContainer)
        {
            _gridProvider = gridProvider;
        }

        public override bool IsDimmed
        {
            get
            {
                foreach (var pos in Position.Positions)
                {
                    if (_gridProvider.GetCandidatesCount(pos) == 2)
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