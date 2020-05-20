﻿using System.Threading.Tasks;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class RestartCommand : ICommand
    {
        private readonly ISudokuProvider _sudokuProvider;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly IGameTimerProvider _gameTimerProvider;

        public RestartCommand(
            ISudokuProvider sudokuProvider, 
            IGridHistoryManager gridHistoryManager,
            IGameTimerProvider gameTimerProvider)
        {
            _sudokuProvider = sudokuProvider;
            _gridHistoryManager = gridHistoryManager;
            _gameTimerProvider = gameTimerProvider;
        }
        public Task Execute()
        {
            _sudokuProvider.Restart();
            _sudokuProvider.ClearCandidates();
            _gridHistoryManager.ClearUndo();
            _gameTimerProvider.Start();            
            return Task.CompletedTask;
        }
    }
}
