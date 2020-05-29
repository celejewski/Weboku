using System;
using System.Security.Cryptography.X509Certificates;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class EraserAction : IClickableAction
    {
        private readonly ISudokuProvider _sudokuProvider;
        private readonly IGridHistoryManager _gridHistoryManager;

        public EraserAction(ISudokuProvider sudokuProvider, IGridHistoryManager gridHistoryManager)
        {
            _sudokuProvider = sudokuProvider;
            _gridHistoryManager = gridHistoryManager;
        }
        
        public void LeftClickAction(ClickableActionArgs args)
        {
            var cell = _sudokuProvider.Cells[args.X, args.Y];
            if (cell.Input.Value == 0)
            {
                _gridHistoryManager.Save();
                _sudokuProvider.ToggleCandidate(args.X, args.Y, args.Value);
            }
        }

        public void RightClickAction(ClickableActionArgs args)
        {
            var cell = _sudokuProvider.Cells[args.X, args.Y];
            if (cell.Input.Value == 0)
            {
                _gridHistoryManager.Save();
                _sudokuProvider.SetValue(args.X, args.Y, args.Value);
            }
            else if (cell.Input.Value == args.Value && !cell.IsGiven)
            {
                _gridHistoryManager.Save();
                _sudokuProvider.SetValue(args.X, args.Y, 0);
            }
        }
    }
}
