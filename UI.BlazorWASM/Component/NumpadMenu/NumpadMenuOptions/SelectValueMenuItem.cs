using System.Linq;
using Weboku.Application;
using Weboku.Core.Data;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions
{
    public class SelectValueMenuItem : NumpadMenuLabel
    {
        private readonly Value _value;
        private readonly DomainFacade _gridProvider;

        public SelectValueMenuItem(Value value, DomainFacade gridProvider, MenuOptionSettings menuOptionSettings)
            : base(menuOptionSettings)
        {
            _value = value;
            _gridProvider = gridProvider;
        }

        public override bool IsDimmed
        {
            get
            {
                foreach (var row in Position.Rows)
                {
                    var isAnyValueIllegal = !row.All(_gridProvider.IsValueLegal);
                    var legalValuesInRow = row.Count(pos => _gridProvider.GetValue(pos) == _value);
                    if (isAnyValueIllegal || legalValuesInRow != 1) return false;
                }

                return true;
            }
        }

        public override bool IsSelectable => true;

        public string Label => _value.ToString();
        public override string Tooltip => "select-value__tooltip";
    }
}