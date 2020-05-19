using System.Threading.Tasks;
using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Component.NumpadMenu
{
    public abstract class BaseMenuOption : INumpadMenuItem, ICommand
    {
        private readonly NumpadMenuProvider _numpadMenuProvider;
        private readonly ICommand _command;

        protected BaseMenuOption(NumpadMenuProvider numpadMenuProvider, ICommand command)
        {
            _numpadMenuProvider = numpadMenuProvider;
            _command = command;
        }

        public abstract bool IsDimmed { get; }
        public abstract bool IsSelectable { get; }

        public async Task Execute()
        {
            if( IsSelectable )
            {
                _numpadMenuProvider.SelectItem(this);
            }

            await _command.Execute();
        }
    }
}
