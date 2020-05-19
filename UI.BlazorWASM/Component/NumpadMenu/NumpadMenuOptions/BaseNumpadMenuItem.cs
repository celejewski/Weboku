using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public abstract class BaseNumpadMenuItem : INumpadMenuItem
    {
        private readonly NumpadMenuProvider _numpadMenuProvider;

        protected BaseNumpadMenuItem(NumpadMenuProvider numpadMenuProvider)
        {
            _numpadMenuProvider = numpadMenuProvider;
        }

        public abstract bool IsDimmed { get; }
        public abstract bool IsSelectable { get; }

        public virtual Task Execute()
        {
            if( IsSelectable )
            {
                _numpadMenuProvider.SelectItem(this);
            }
            return Task.CompletedTask;
        }
    }
}
