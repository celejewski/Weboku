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

        public SelectColorCommand(CellColor cellColor, IClickableActionProvider clickableActionProvider)
        {
            _cellColor = cellColor;
            _clickableActionProvider = clickableActionProvider;
        }
        public Task Execute()
        {
            _clickableActionProvider.Color1 = _cellColor;
            _clickableActionProvider.Color2 = _cellColor;
            return Task.CompletedTask;
        }
    }
}
