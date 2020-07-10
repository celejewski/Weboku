using Core.Data;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class CleanerAction : IClickableAction
    {
        private readonly IGridProvider _gridProvider;
        private readonly IGridHistoryManager _gridHistoryManager;

        private void Clear(Position pos)
        {
            if( _gridProvider.GetIsGiven(pos) ) return;
            _gridHistoryManager.Save();
            _gridProvider.ClearCandidates(pos);
            _gridProvider.SetValue(pos, InputValue.Empty);
        }
        public CleanerAction(IGridProvider gridProvider, IGridHistoryManager gridHistoryManager)
        {
            _gridProvider = gridProvider;
            _gridHistoryManager = gridHistoryManager;
        }

        public void LeftClickAction(ClickableActionArgs args)
        {
            Clear(args.Pos);
        }

        public void RightClickAction(ClickableActionArgs args)
        {
            Clear(args.Pos);
        }
    }
}
