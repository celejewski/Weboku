using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class SelectValueNumpadMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        private readonly int _value;
        private readonly ISudokuProvider _sudokuProvider;

        public SelectValueNumpadMenuItem(int value, ISudokuProvider sudokuProvider, NumpadMenuProvider numpadMenuProvider , CommandProvider commandProvider ) 
            : base(numpadMenuProvider, commandProvider.SelectValue(value))
        {
            _value = value;
            _sudokuProvider = sudokuProvider;
        }

        public override bool IsDimmed
        { 
            get
            {
                int count = 0;
                for( int y = 0; y < 9; y++ )
                {
                    for( int x = 0; x < 9; x++ )
                    {
                        var cell = _sudokuProvider.Cells[x, y];
                        if (cell.Input.Value == _value
                            && cell.Input.IsLegal)
                        {
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
