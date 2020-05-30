using Core.Data;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class StandardAction : IClickableAction
    {
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly ICellColorProvider _cellColorProvider;
        private readonly ISudokuProvider _sudokuProvider;

        public StandardAction(IGridHistoryManager gridHistoryManager, ICellColorProvider cellColorProvider, ISudokuProvider sudokuProvider)
        {
            _gridHistoryManager = gridHistoryManager;
            _cellColorProvider = cellColorProvider;
            _sudokuProvider = sudokuProvider;
        }
        public void LeftClickAction(ClickableActionArgs args)
        {
            if( args.MouseEventArgs.CtrlKey )
            {
                _cellColorProvider.ToggleColor(args.X, args.Y, args.Color1);
                return;
            }

            var cell = _sudokuProvider.Cells[args.X, args.Y];
            if( cell.IsGiven )
            {
                return;
            }

            if( args.Value == 0 || cell.Input.Value == 0 )
            {
                _gridHistoryManager.Save();
                _sudokuProvider.SetValue(cell.X, cell.Y, args.Value);
            }
            else if( cell.Input.Value == args.Value )
            {
                _gridHistoryManager.Save();
                _sudokuProvider.SetValue(cell.X, cell.Y, 0);
            }
        }
        public void RightClickAction(ClickableActionArgs args)
        {
            if( args.MouseEventArgs.CtrlKey )
            {
                _cellColorProvider.ToggleColor(args.X, args.Y, args.Color2);
                return;
            }

            var cell = _sudokuProvider.Cells[args.X, args.Y];
            if( cell.Input.Value != 0 || args.Value == 0 )
            {
                return;
            }

            _gridHistoryManager.Save();
            _sudokuProvider.ToggleCandidate(cell.X, cell.Y, args.Value);
        }
    }
}
