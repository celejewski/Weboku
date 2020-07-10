using Core.Data;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class EraserAction : IClickableAction
    {
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly IGridProvider _gridProvider;

        public EraserAction(IGridHistoryManager gridHistoryManager, IGridProvider gridProvider)
        {
            _gridHistoryManager = gridHistoryManager;
            _gridProvider = gridProvider;
        }

        public void LeftClickAction(ClickableActionArgs args)
        {
            if( _gridProvider.GetIsGiven(args.Pos) ) return;

            if( _gridProvider.GetValue(args.Pos) == InputValue.Empty )
            {
                _gridHistoryManager.Save();
                _gridProvider.ToggleCandidate(args.Pos, args.Value);
            }
        }

        public void RightClickAction(ClickableActionArgs args)
        {
            if( _gridProvider.GetIsGiven(args.Pos) ) return;

            if( _gridProvider.GetValue(args.Pos) == InputValue.Empty )
            {
                _gridHistoryManager.Save();
                _gridProvider.SetValue(args.Pos, args.Value);
            }
            else if( _gridProvider.GetValue(args.Pos) == args.Value )
            {
                _gridHistoryManager.Save();
                _gridProvider.SetValue(args.Pos, InputValue.Empty);
            }
        }
    }
}
