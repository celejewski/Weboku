using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class EraseAction : IClickableAction
    {
        private readonly ISudokuProvider _sudokuProvider;
        private readonly IGridHistoryManager _gridHistoryManager;

        private void Erase(int x, int y)
        {
            _gridHistoryManager.Save();
            _sudokuProvider.Cells[x,y].Candidates.Clear();
            _sudokuProvider.SetValue(x, y, 0);
        }
        public EraseAction(ISudokuProvider sudokuProvider, IGridHistoryManager gridHistoryManager)
        {
            _sudokuProvider = sudokuProvider;
            _gridHistoryManager = gridHistoryManager;
        }

        public void LeftClickAction(MouseEventArgs e, int x, int y)
        {
            Erase(x, y);
        }

        public void RightClickAction(MouseEventArgs e, int x, int y)
        {
            Erase(x, y);
        }
    }
}
