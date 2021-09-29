using Weboku.Application;
using Weboku.Core.Data;

namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public class PairsFilterMenuItem : NumpadMenuLabel
    {
        private readonly DomainFacade _gridProvider;

        public PairsFilterMenuItem(MenuOptionSettings menuOptionSettings, DomainFacade gridProvider)
            : base(menuOptionSettings)
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