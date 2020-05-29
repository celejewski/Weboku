using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class SelectColorCommand : ICommand
    {
        private readonly CellColor _cellColor;
        private readonly IClickableActionProvider _clickableActionProvider;
        private readonly ClickableActionFactory _clickableActionFactory;

        public SelectColorCommand(CellColor cellColor, IClickableActionProvider clickableActionProvider, ClickableActionFactory clickableActionFactory)
        {
            _cellColor = cellColor;
            _clickableActionProvider = clickableActionProvider;
            _clickableActionFactory = clickableActionFactory;
        }
        public Task Execute()
        {
            _clickableActionProvider.SetClickableAction(_clickableActionFactory.ColorAction(_cellColor));
            return Task.CompletedTask;
        }
    }
}
