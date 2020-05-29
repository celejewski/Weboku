using Core.Data;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class StandardAction : IClickableAction
    {
        private readonly int _selectedValue;
        private readonly ICell[,] _cells;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly ICellColorProvider _cellColorProvider;
        private readonly ISudokuProvider _sudokuProvider;

        public StandardAction(IGridHistoryManager gridHistoryManager, ICellColorProvider cellColorProvider, ISudokuProvider sudokuProvider, int selectedValue)
        {
            _cells = sudokuProvider.Cells;
            _gridHistoryManager = gridHistoryManager;
            _cellColorProvider = cellColorProvider;
            _sudokuProvider = sudokuProvider;
            _selectedValue = selectedValue;
        }
        public void LeftClickAction(ClickableActionArgs args)
        {
            if( args.MouseEventArgs.CtrlKey )
            {
                _cellColorProvider.ToggleColor(args.X, args.Y, CellColor.First);
                return;
            }

            var cell = _cells[args.X, args.Y];
            if( cell.IsGiven )
            {
                return;
            }

            if( _selectedValue == 0 || cell.Input.Value == 0 )
            {
                _gridHistoryManager.Save();
                _sudokuProvider.SetValue(cell.X, cell.Y, _selectedValue);
            }
            else if( cell.Input.Value == _selectedValue )
            {
                _gridHistoryManager.Save();
                _sudokuProvider.SetValue(cell.X, cell.Y, 0);
            }
        }
        public void RightClickAction(ClickableActionArgs args)
        {
            if( args.MouseEventArgs.CtrlKey )
            {
                _cellColorProvider.ToggleColor(args.X, args.Y, CellColor.Second);
                return;
            }

            var cell = _cells[args.X, args.Y];
            if( cell.Input.Value != 0 || _selectedValue == 0 )
            {
                return;
            }

            _gridHistoryManager.Save();
            _sudokuProvider.ToggleCandidate(cell.X, cell.Y, _selectedValue);
        }
    }
}
