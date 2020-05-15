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
        private Action _executeAction;
        private Func<bool> _canExecutePredicate;
        private Func<bool> _isDimmedPredicate;
        private IClickableAction _clickableAction;
        private bool _isSelectable;
        private string _label;
        private readonly IFilterProvider _filterProvider;
        private readonly IClickableActionProvider _clickableActionProvider;

        public NumpadMenuItemBuilder(IFilterProvider filterProvider, IClickableActionProvider clickableActionProvider)
        {
            _filterProvider = filterProvider;
            _clickableActionProvider = clickableActionProvider;
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

        public NumpadMenuItemBuilder WithChangeFilter(IFilter filter)
        {
            _filter = filter;
            return this;
        }

        public NumpadMenuItemBuilder WithChangeAction(IClickableAction action)
        {
            _clickableAction = action;
            return this;
        }

        public NumpadMenuItemBuilder WithIsDimmed(Func<bool> isDimmedPredicate)
        {
            _isDimmedPredicate = isDimmedPredicate;
            return this;
        }

        public INumpadMenuItem Build()
        {
            return new NumpadMenuItem(_label, _executeAction, _canExecutePredicate, _isDimmedPredicate, _clickableAction, _isSelectable, _filter, _filterProvider, _clickableActionProvider);
        }
    }
}
