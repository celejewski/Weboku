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
            if (_gridProvider.GetValue(args.X, args.Y) == InputValue.Empty)
            {
                _gridHistoryManager.Save();
                _gridProvider.ToggleCandidate(args.X, args.Y, args.Value);
            }
        }

        public void RightClickAction(ClickableActionArgs args)
        {
            if (_gridProvider.GetValue(args.X, args.Y) == InputValue.Empty)
            {
                _gridHistoryManager.Save();
                _gridProvider.SetValue(args.X, args.Y, args.Value);
            }
            else if (_gridProvider.GetValue(args.X, args.Y) == args.Value && _gridProvider.IsGiven(args.X, args.Y))
            {
                _gridHistoryManager.Save();
                _gridProvider.SetValue(args.X, args.Y, InputValue.Empty);
            }
        }
    }
}
