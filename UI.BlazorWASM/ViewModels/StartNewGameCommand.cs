using Core.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ViewModels
{
    public class StartNewGameCommand : ICommand
    {
        private readonly string _difficulty;
        private readonly IGridGenerator _generator;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly ISudokuProvider _sudokuProvider;
        private readonly IGameTimerProvider _gameTimerProvider;

        public StartNewGameCommand(string difficulty, IGridGenerator generator, IGridHistoryManager gridHistoryManager, ISudokuProvider sudokuProvider, IGameTimerProvider gameTimerProvider)
        {
            _difficulty = difficulty;
            _generator = generator;
            _gridHistoryManager = gridHistoryManager;
            _sudokuProvider = sudokuProvider;
            _gameTimerProvider = gameTimerProvider;
        }
        
        public bool CanExecute => true;

        public async Task Execute()
        {
            var newGrid = await _generator.WithGiven(_difficulty);
            _gridHistoryManager.Save();
            _sudokuProvider.AssignFrom(newGrid);
            _gameTimerProvider.Start();
        }
    }
}
