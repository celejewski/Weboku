using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ViewModels
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
