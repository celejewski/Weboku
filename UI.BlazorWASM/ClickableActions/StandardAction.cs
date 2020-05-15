using Core.Data;
using Microsoft.AspNetCore.Components.Web;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class StandardAction : IClickableAction
    {
        private readonly ICell[,] _cells;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly ICellColorProvider _cellColorProvider;
        private readonly ISudokuProvider _sudokuProvider;

        public StandardAction(IGridHistoryManager gridHistoryManager, ICellColorProvider cellColorProvider, ISudokuProvider sudokuProvider)
        {
            _cells = sudokuProvider.Cells;
            _gridHistoryManager = gridHistoryManager;
            _cellColorProvider = cellColorProvider;
            _sudokuProvider = sudokuProvider;
        }
        public int SelectedValue { get; set; } = 1;
        public void LeftClickAction(MouseEventArgs e, int x, int y)
        {
            if( e.CtrlKey )
            {
                _cellColorProvider.ToggleColor(x, y, CellColor.First);
                return;
            }

            var cell = _cells[x, y];
            if( cell.IsGiven )
            {
                return;
            }

            if( SelectedValue == 0 || cell.Input.Value == 0 )
            {
                _gridHistoryManager.Save();
                _sudokuProvider.SetValue(cell.X, cell.Y, SelectedValue);
            }
            else if( cell.Input.Value == SelectedValue )
            {
                _gridHistoryManager.Save();
                _sudokuProvider.SetValue(cell.X, cell.Y, 0);
            }
        }
        public void RightClickAction(MouseEventArgs e, int x, int y)
        {
            if( e.CtrlKey )
            {
                _cellColorProvider.ToggleColor(x, y, CellColor.Second);
                return;
            }

            var cell = _cells[x, y];
            if( cell.Input.Value != 0 || SelectedValue == 0 )
            {
                return;
            }

            _gridHistoryManager.Save();
            _sudokuProvider.ToggleCandidate(cell.X, cell.Y, SelectedValue);
        }
    }
}
