using Microsoft.AspNetCore.Components.Web;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class CleanerAction : IClickableAction
    {
        private readonly ISudokuProvider _sudokuProvider;
        private readonly IGridHistoryManager _gridHistoryManager;

        private void Clear(int x, int y)
        {
            _gridHistoryManager.Save();
            _sudokuProvider.Cells[x,y].Candidates.Clear();
            _sudokuProvider.SetValue(x, y, 0);
        }
        public CleanerAction(ISudokuProvider sudokuProvider, IGridHistoryManager gridHistoryManager)
        {
            _sudokuProvider = sudokuProvider;
            _gridHistoryManager = gridHistoryManager;
        }

        public void LeftClickAction(MouseEventArgs e, int x, int y)
        {
            Clear(x, y);
        }

        public void RightClickAction(MouseEventArgs e, int x, int y)
        {
            Clear(x, y);
        }
    }
}
