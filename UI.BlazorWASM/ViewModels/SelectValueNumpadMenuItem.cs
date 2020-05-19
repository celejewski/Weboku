using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Filters;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ViewModels
{
    public class SelectValueNumpadMenuItem : BaseNumpadMenuItem, INumpadMenuLabel
    {
        private readonly int _value;
        private readonly IFilterProvider _filterProvider;
        private readonly IClickableActionProvider _clickableActionProvider;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly ICellColorProvider _cellColorProvider;
        private readonly ISudokuProvider _sudokuProvider;

        public SelectValueNumpadMenuItem(int value, IFilterProvider filterProvider, IClickableActionProvider clickableActionProvider, IGridHistoryManager gridHistoryManager, ICellColorProvider cellColorProvider, ISudokuProvider sudokuProvider, NumpadMenuProvider numpadMenuProvider ) : base(numpadMenuProvider)
        {
            _value = value;
            _filterProvider = filterProvider;
            _clickableActionProvider = clickableActionProvider;
            _gridHistoryManager = gridHistoryManager;
            _cellColorProvider = cellColorProvider;
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

        public override async Task Execute()
        {
            await base.Execute();
            _filterProvider.SetFilter(new SelectedValueFilter(_value));
            _clickableActionProvider.SetClickableAction(new StandardAction(_gridHistoryManager, _cellColorProvider, _sudokuProvider, _value));
        }
    }
}
