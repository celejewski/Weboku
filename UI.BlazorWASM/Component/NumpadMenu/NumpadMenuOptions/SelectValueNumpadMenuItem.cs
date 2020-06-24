using Core.Data;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class SelectValueNumpadMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        private readonly InputValue _value;
        private readonly IGridProvider _gridProvider;

        public SelectValueNumpadMenuItem(InputValue value, IGridProvider gridProvider, NumpadMenuProvider numpadMenuProvider, CommandProvider commandProvider)
            : base(commandProvider.SelectValue(value), numpadMenuProvider.FilterContainer)
        {
            _value = value;
            _gridProvider = gridProvider;
        }

        public override bool IsDimmed
        {
            get
            {
                int count = 0;
                for( int y = 0; y < 9; y++ )
                {
                    if( count != y )
                    {
                        return false;
                    }

                    for( int x = 0; x < 9; x++ )
                    {
                        if( _gridProvider.GetValue(x, y) == _value )
                        {
                            if( !_gridProvider.IsValueLegal(x, y) )
                            {
                                return false;
                            }
                            count += 1;
                        }
                    }
                }
                return count == 9;
            }
        }

        public override bool IsSelectable => true;

        public string Label => _value.ToString();
    }
}
