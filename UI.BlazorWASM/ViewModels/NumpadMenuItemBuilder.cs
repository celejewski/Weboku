using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.Filters;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ViewModels
{
    public class NumpadMenuItemBuilder 
    {
        private IFilter _filter;
        private EventCallback<IFilter> _filterChangedEvent;
        private Action _executeAction;
        private Func<bool> _canExecutePredicate;
        private Func<bool> _isDimmedPredicate;
        private IClickableAction _clickableAction;
        private EventCallback<IClickableAction> _clickableActionChangedEvent;
        private Action<IClickableAction> _clickableActionChangedAction;
        private bool _isSelectable;
        private string _label;
        private readonly IFilterProvider _filterProvider;

        public NumpadMenuItemBuilder(IFilterProvider filterProvider)
        {
            _filterProvider = filterProvider;
        }

        public NumpadMenuItemBuilder SetIsSelectable(bool isSelectable)
        {
            _isSelectable = isSelectable;
            return this;
        }

        public NumpadMenuItemBuilder SetLabel(string label)
        {
            _label = label;
            return this;
        }

        public NumpadMenuItemBuilder WithCanExecute(Func<bool> canExecutePredicate)
        {
            _canExecutePredicate = canExecutePredicate;
            return this;
        }
        public NumpadMenuItemBuilder WithExecute(Action executeAction)
        {
            _executeAction = executeAction;
            return this;
        }
        public NumpadMenuItemBuilder WithChangeFilter(EventCallback<IFilter> filterChanged, IFilter filter)
        {
            _filter = filter;
            _filterChangedEvent = filterChanged;
            return this;
        }
        public NumpadMenuItemBuilder WithChangeFilter(IFilter filter)
        {
            _filter = filter;
            return this;
        }

        public NumpadMenuItemBuilder WithChangeAction(EventCallback<IClickableAction> clickableActionChanged, IClickableAction action)
        {
            _clickableAction = action;
            _clickableActionChangedEvent = clickableActionChanged;
            return this;
        }
        public NumpadMenuItemBuilder WithChangeAction(Action<IClickableAction> clickableActionChanged, IClickableAction action)
        {
            _clickableAction = action;
            _clickableActionChangedAction = clickableActionChanged;
            return this;
        }
        public NumpadMenuItemBuilder WithIsDimmed(Func<bool> isDimmedPredicate)
        {
            _isDimmedPredicate = isDimmedPredicate;
            return this;
        }

        public INumpadMenuItem Build()
        {
            return new NumpadMenuItem(_label, _executeAction, _canExecutePredicate, _isDimmedPredicate, _clickableAction, _clickableActionChangedAction, _isSelectable, _filter, _filterProvider);
        }
    }
}
