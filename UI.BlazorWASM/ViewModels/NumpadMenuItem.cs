using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Filters;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ViewModels
{
    public class NumpadMenuItem : INumpadMenuItem
    {
        private readonly IFilter _filter;
        private readonly Action _executeAction;
        private readonly Func<bool> _canExecutePredicate;
        private readonly Func<bool> _isDimmedPredicate;
        private readonly IClickableAction _clickableAction;
        private readonly Action<IClickableAction> _clickableActionChangedAction;
        private readonly bool _isSelectable;
        private readonly IFilterProvider _filterProvider;

        public bool IsDimmed => _isDimmedPredicate?.Invoke() ?? false;
        public bool IsSelectable { get; private set; } = true;
        public string Label { get; private set; }
        public bool CanExecute => _canExecutePredicate?.Invoke() ?? true;

        public async Task Execute()
        {
            _executeAction?.Invoke();
            _clickableActionChangedAction?.Invoke(_clickableAction);

            if (_filter != null)
            {
                _filterProvider.SetFilter(_filter);
            }
        }

        public NumpadMenuItem(string label, Action executeAction, Func<bool> canExecutePredicate, Func<bool> isDimmedPredicate, IClickableAction clickableAction, Action<IClickableAction> clickableActionChangedAction, bool isSelectable, IFilter filter, IFilterProvider filterProvider)
        {
            Label = label;
            _executeAction = executeAction;
            _canExecutePredicate = canExecutePredicate;
            _isDimmedPredicate = isDimmedPredicate;
            _clickableAction = clickableAction;
            _clickableActionChangedAction = clickableActionChangedAction;
            _isSelectable = isSelectable;
            _filter = filter;
            _filterProvider = filterProvider;
        }
    }
}
