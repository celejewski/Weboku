using Core.Data;
using Microsoft.AspNetCore.Components.Web;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class CleanerAction : IClickableAction
    {
        private readonly IGridProvider _gridProvider;
        private readonly IGridHistoryManager _gridHistoryManager;

        private void Clear(int x, int y)
        {
            _gridHistoryManager.Save();
            _gridProvider.RemoveCandidates(x, y);
            _gridProvider.SetValue(x, y, InputValue.Empty);
        }
        public CleanerAction(IGridProvider gridProvider, IGridHistoryManager gridHistoryManager)
        {
            _gridProvider = gridProvider;
            _gridHistoryManager = gridHistoryManager;
        }

        public void LeftClickAction(ClickableActionArgs args)
        {
            Clear(args.X, args.Y);
        }

        public void RightClickAction(ClickableActionArgs args)
        {
            Clear(args.X, args.Y);
        }
    }
}
