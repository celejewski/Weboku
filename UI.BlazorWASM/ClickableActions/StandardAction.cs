using Core.Data;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class StandardAction : IClickableAction
    {
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly CellColorProvider _cellColorProvider;
        private readonly IGridProvider _gridProvider;

        public StandardAction(IGridHistoryManager gridHistoryManager, CellColorProvider cellColorProvider, IGridProvider gridProvider)
        {
            _gridHistoryManager = gridHistoryManager;
            _cellColorProvider = cellColorProvider;
            _gridProvider = gridProvider;
        }
        public void LeftClickAction(ClickableActionArgs args)
        {
            if( args.MouseEventArgs.CtrlKey )
            {
                _cellColorProvider.ToggleColor(args.X, args.Y, args.Color1);
                return;
            }

            if( _gridProvider.GetIsGiven(args.X, args.Y))
            {
                return;
            }

            if( args.Value == InputValue.Empty || _gridProvider.GetValue(args.X, args.Y) == InputValue.Empty)
            {
                _gridHistoryManager.Save();
                _gridProvider.SetValue(args.X, args.Y, args.Value);
            }
            else if( _gridProvider.GetValue(args.X, args.Y) == args.Value )
            {
                _gridHistoryManager.Save();
                _gridProvider.SetValue(args.X, args.Y, 0);
            }
        }
        public void RightClickAction(ClickableActionArgs args)
        {
            if( args.MouseEventArgs.CtrlKey )
            {
                _cellColorProvider.ToggleColor(args.X, args.Y, args.Color2);
                return;
            }

            
            if( _gridProvider.GetValue(args.X, args.Y) != InputValue.Empty || args.Value == InputValue.Empty )
            {
                return;
            }

            _gridHistoryManager.Save();
            _gridProvider.ToggleCandidate(args.X, args.Y, args.Value);
        }
    }
}
