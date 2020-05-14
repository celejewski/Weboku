using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.Filters;
using UI.BlazorWASM.ClickableActions;

namespace UI.BlazorWASM.ViewModels
{
    public class NumpadMenuItem : INumpadMenuItem
    {
        private IFilter _filter;
        private EventCallback<IFilter> _filterChangedEvent;
        private Action<IFilter> _filterChangedAction;
        private Action _executeAction;
        private Func<bool> _canExecutePredicate;
        private Func<bool> _isDimmedPredicate;
        private IClickableAction _clickableAction;
        private EventCallback<IClickableAction> _clickableActionChangedEvent;
        private Action<IClickableAction> _clickableActionChangedAction;
        
        public bool IsDimmed => _isDimmedPredicate?.Invoke() ?? false;
        public bool IsSelectable { get; private set; } = true;
        public string Label { get; private set; }
        public bool CanExecute => _canExecutePredicate?.Invoke() ?? true;
        public async Task Execute()
        {
            _executeAction?.Invoke();
            await _clickableActionChangedEvent.InvokeAsync(_clickableAction);
            _clickableActionChangedAction?.Invoke(_clickableAction);
            await _filterChangedEvent.InvokeAsync(_filter);
            _filterChangedAction?.Invoke(_filter);
        }

        public NumpadMenuItem SetIsSelectable(bool isSelectable)
        {
            IsSelectable = isSelectable;
            return this;
        }

        public NumpadMenuItem SetLabel(string label)
        {
            Label = label;
            return this;
        }

        public NumpadMenuItem WithCanExecute(Func<bool> canExecutePredicate)
        {
            _canExecutePredicate = canExecutePredicate;
            return this;
        }
        public NumpadMenuItem WithExecute(Action executeAction)
        {
            _executeAction = executeAction;
            return this;
        }
        public NumpadMenuItem WithChangeFilter(EventCallback<IFilter> filterChanged, IFilter filter)
        {
            _filter = filter;
            _filterChangedEvent = filterChanged;
            return this;
        }
        public NumpadMenuItem WithChangeFilter(Action<IFilter> filterChanged, IFilter filter)
        {
            _filter = filter;
            _filterChangedAction = filterChanged;
            return this;
        }

        public NumpadMenuItem WithChangeAction(EventCallback<IClickableAction> clickableActionChanged, IClickableAction action)
        {
            _clickableAction = action;
            _clickableActionChangedEvent = clickableActionChanged;
            return this;
        }
        public NumpadMenuItem WithChangeAction(Action<IClickableAction> clickableActionChanged, IClickableAction action)
        {
            _clickableAction = action;
            _clickableActionChangedAction = clickableActionChanged;
            return this;
        }
        public NumpadMenuItem WithIsDimmed(Func<bool> isDimmedPredicate)
        {
            _isDimmedPredicate = isDimmedPredicate;
            return this;
        }

    }
}
