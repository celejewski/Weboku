using Core;
using Core.Data;
using System.Linq;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class SelectValueMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        private readonly InputValue _value;
        private readonly DomainFacade _gridProvider;

        public SelectValueMenuItem(InputValue value, DomainFacade gridProvider, NumpadMenuProvider numpadMenuProvider, CommandProvider commandProvider)
            : base(commandProvider.SelectValue(value), numpadMenuProvider.FilterContainer)
        {
            _value = value;
            _gridProvider = gridProvider;
        }

        public override bool IsDimmed
        {
            get
            {
                foreach( var row in Position.Rows )
                {
                    var isAnyValueIllegal = !row.All(_gridProvider.IsValueLegal);
                    var legalValuesInRow = row.Count(pos => _gridProvider.GetInputValue(pos) == _value);
                    if( isAnyValueIllegal || legalValuesInRow != 1 ) return false;
                }
                return true;
            }
        }

        public override bool IsSelectable => true;

        public string Label => _value.ToString();
        public override string Tooltip => "select-value__tooltip";
    }
}
