using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Filters;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class SelectEraseCommand : ICommand
    {
        private readonly IClickableActionProvider _clickableActionProvider;
        private readonly ClickableActionFactory _clickableActionFactory;
        private readonly IFilterProvider _filterProvider;

        public SelectEraseCommand(
            IClickableActionProvider clickableActionProvider,
            ClickableActionFactory clickableActionFactory,
            IFilterProvider filterProvider)
        {
            _clickableActionProvider = clickableActionProvider;
            _clickableActionFactory = clickableActionFactory;
            _filterProvider = filterProvider;
        }
        public Task Execute()
        {
            _clickableActionProvider.SetClickableAction(_clickableActionFactory.EraseAction());
            _filterProvider.SetFilter(new EraseFilter());
            return Task.CompletedTask;
        }
    }
}
