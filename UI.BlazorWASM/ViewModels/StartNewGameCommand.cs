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
        public StartNewGameCommand(string difficulty, IGridGenerator generator, IGridHistoryManager gridHistoryManager, ISudokuProvider sudokuProvider)
        {
            _difficulty = difficulty;
            _generator = generator;
            _gridHistoryManager = gridHistoryManager;
            _sudokuProvider = sudokuProvider;
        }
        
        public bool CanExecute => true;

        public async Task Execute()
        {
            _gridHistoryManager.Save();
            var newGrid = await _generator.WithGiven(_difficulty);
            _sudokuProvider.AssignFrom(newGrid);
        }
    }
}
