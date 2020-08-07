using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class RedoNumpadMenuItem : BaseMenuOption, INumpadMenuLabel
    {
        private readonly GridHistoryProvider _gridHistoryManager;

        public RedoNumpadMenuItem(RedoCommand command, GridHistoryProvider gridHistoryManager)
            : base(command)
        {
            _gridHistoryManager = gridHistoryManager;
        }
        public override bool IsDimmed => !_gridHistoryManager.CanRedo;

        public override bool IsSelectable => false;

        public string Label => "numpad-redo__label";
        public override string Tooltip => "numpad-redo__tooltip"; 
    }
}
