using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.Managers;

namespace UI.BlazorWASM.Commands
{
    public class UndoCommand : ICommand
    {
        private readonly IGridHistoryManager _gridHistoryManager;

        public UndoCommand(IGridHistoryManager gridHistoryManager)
        {
            _gridHistoryManager = gridHistoryManager;
        }
        public Task Execute()
        {
            _gridHistoryManager.Undo();
            return Task.CompletedTask;
        }
    }
}
