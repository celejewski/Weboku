using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ViewModels
{
    public class FindAllCandidatesCommand : ICommand
    {
        private readonly ISudokuProvider _sudokuProvider;
        private readonly IGridHistoryManager _gridHistoryManager;

        public FindAllCandidatesCommand(ISudokuProvider sudokuProvider, IGridHistoryManager gridHistoryManager)
        {
            _sudokuProvider = sudokuProvider;
            _gridHistoryManager = gridHistoryManager;
        }

        public bool CanExecute => true;

        public Task Execute()
        {
            _gridHistoryManager.Save();
            _sudokuProvider.FillAllCandidates();
            return Task.CompletedTask;
        }
    }
}
