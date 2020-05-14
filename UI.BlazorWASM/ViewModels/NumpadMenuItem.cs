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

        private Func<bool> _isDimmedPredicate;
        public NumpadMenuItem SetIsDimmedPredicate(Func<bool> isDimmedPredicate)
        {
            _isDimmedPredicate = isDimmedPredicate;
            return this;
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

        private Func<bool> _canExecutePredicate;
        public NumpadMenuItem WithCanExecute(Func<bool> canExecutePredicate)
        {
            _canExecutePredicate = canExecutePredicate;
            return this;
        }

        public bool IsDimmed => _isDimmedPredicate?.Invoke() ?? false;

        public bool IsSelectable { get; private set; } = true;

        public string Label { get; private set; }

        public bool CanExecute => _canExecutePredicate?.Invoke() ?? true;


        private IClickableAction _clickableAction;
        private EventCallback<IClickableAction> _clickableActionChangedEvent;

        public NumpadMenuItem WithChangeAction(EventCallback<IClickableAction> clickableActionChanged, IClickableAction action)
        {
            _clickableAction = action;
            _clickableActionChangedEvent = clickableActionChanged;
            return this;
        }

        private Action<IClickableAction> _clickableActionChangedAction;
        public NumpadMenuItem WithChangeAction(Action<IClickableAction> clickableActionChanged, IClickableAction action)
        {
            _clickableAction = action;
            _clickableActionChangedAction = clickableActionChanged;
            return this;
        }

        private IFilter _filter;
        private EventCallback<IFilter> _filterChangedEvent;
        public NumpadMenuItem WithChangeFilter(EventCallback<IFilter> filterChanged, IFilter filter)
        {
            _filter = filter;
            _filterChangedEvent = filterChanged;
            return this;
        }

        private Action<IFilter> _filterChangedAction;
        public NumpadMenuItem WithChangeFilter(Action<IFilter> filterChanged, IFilter filter)
        {
            _filter = filter;
            _filterChangedAction = filterChanged;
            return this;
        }

        private Action _executeAction;
        public NumpadMenuItem WithExecute(Action executeAction)
        {
            _executeAction = executeAction;
            return this;
        }

        public async Task Execute()
        {
            _executeAction?.Invoke();
            await _clickableActionChangedEvent.InvokeAsync(_clickableAction);
            _clickableActionChangedAction?.Invoke(_clickableAction);
            await _filterChangedEvent.InvokeAsync(_filter);
            _filterChangedAction?.Invoke(_filter);
            
        }
    }
}
