using Core.Data;
using Core.Managers;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.BlazorWASM.ClickableActions
{
    public class StandardAction : IClickableAction
    {
        private readonly string[,] _colorClasses;
        private readonly ICell[,] _cells;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly IGrid _grid;
        private readonly Action _stateHasChanged;

        public StandardAction(string[,] colorClasses, ICell[,] cells, IGridHistoryManager gridHistoryManager, IGrid grid, Action stateHasChanged)
        {
            _colorClasses = colorClasses;
            _cells = cells;
            _gridHistoryManager = gridHistoryManager;
            _grid = grid;
            _stateHasChanged = stateHasChanged;
        }
        public int SelectedValue { get; set; } = 1;
        public void LeftClickAction(MouseEventArgs e, int x, int y)
        {
            if( e.CtrlKey )
            {
                if( string.IsNullOrEmpty(_colorClasses[x, y]) )
                {
                    _colorClasses[x, y] = "color-1";
                }
                else
                {
                    _colorClasses[x, y] = string.Empty;
                }
                _stateHasChanged();
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
                _grid.SetValue(cell.X, cell.Y, SelectedValue);
                _stateHasChanged();
            }
            else if( cell.Input.Value == SelectedValue )
            {
                _gridHistoryManager.Save();
                _grid.SetValue(cell.X, cell.Y, 0);
                _stateHasChanged();
            }
        }
        public void RightClickAction(MouseEventArgs e, int x, int y)
        {

            if( e.CtrlKey )
            {
                if( string.IsNullOrEmpty(_colorClasses[x, y]) )
                {
                    _colorClasses[x, y] = "color-2";
                }
                else
                {
                    _colorClasses[x, y] = string.Empty;
                }
                _stateHasChanged();
                return;
            }

            var cell = _cells[x, y];
            if( cell.Input.Value != 0 || SelectedValue == 0 )
            {
                return;
            }

            _gridHistoryManager.Save();
            _grid.ToggleCandidate(cell.X, cell.Y, SelectedValue);
            _stateHasChanged();
        }
    }
}
