using System.Threading.Tasks;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public class SelectColorMenuItem : ISelectColorMenuItem
    {
        public Color Color1 { get; }
        public Color Color2 { get; }
        private readonly ClickableActionProvider _clickableActionProvider;
        private readonly NumpadMenuProvider _numpadMenuProvider;

        public SelectColorMenuItem(
            Color color1,
            Color color2,
            ClickableActionProvider clickableActionProvider,
            NumpadMenuProvider numpadMenuProvider)
        {
            Color1 = color1;
            Color2 = color2;
            _clickableActionProvider = clickableActionProvider;
            _numpadMenuProvider = numpadMenuProvider;
        }

        public string Tooltip => "change-color__tooltip";
        public bool IsDimmed => false;

        public bool IsSelectable => true;

        public Task Execute()
        {
            _numpadMenuProvider.ColorContainer.SelectItem(this);
            _clickableActionProvider.Color1 = Color1;
            _clickableActionProvider.Color2 = Color2;
            return Task.CompletedTask;
        }
    }
}
