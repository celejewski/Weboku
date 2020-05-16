using Core.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.ViewModels;

namespace UI.BlazorWASM.Providers
{
    public class CommandProvider
    {
        private readonly IGridGenerator _gridGenerator;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly ISudokuProvider _sudokuProvider;

        public CommandProvider(IGridGenerator gridGenerator, IGridHistoryManager gridHistoryManager, ISudokuProvider sudokuProvider)
        {
            _gridGenerator = gridGenerator;
            _gridHistoryManager = gridHistoryManager;
            _sudokuProvider = sudokuProvider;
        }
        public ICommand StartNewGame(string difficulty)
        {
            return new StartNewGameCommand(difficulty, _gridGenerator, _gridHistoryManager, _sudokuProvider);
        }

        public ICommand FindAllCandidates()
        {
            return new FindAllCandidatesCommand(_sudokuProvider, _gridHistoryManager);
        }
    }
}
