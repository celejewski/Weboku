﻿using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class ClearColorsCommand : ICommand
    {
        private readonly CellColorProvider _cellColorProvider;

        public ClearColorsCommand(CellColorProvider cellColorProvider)
        {
            _cellColorProvider = cellColorProvider;
        }

        public Task Execute()
        {
            _cellColorProvider.ClearAll();
            return Task.CompletedTask;
        }
    }
}
