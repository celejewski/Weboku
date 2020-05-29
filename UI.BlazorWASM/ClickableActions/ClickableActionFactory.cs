using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ClickableActions
{
    public class ClickableActionFactory
    {
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly ICellColorProvider _cellColorProvider;
        private readonly ISudokuProvider _sudokuProvider;

        public ClickableActionFactory(IGridHistoryManager gridHistoryManager, ICellColorProvider cellColorProvider, ISudokuProvider sudokuProvider)
        {
            _gridHistoryManager = gridHistoryManager;
            _cellColorProvider = cellColorProvider;
            _sudokuProvider = sudokuProvider;
        }
        
        public IClickableAction ColorAction(CellColor cellColor)
        {
            return new ColorAction(cellColor, _cellColorProvider); 
        }
        public IClickableAction StandardAction(int value)
        {
            return new StandardAction(_gridHistoryManager, _cellColorProvider, _sudokuProvider, value);
        }

        public IClickableAction EraseAction()
        {
            return new CleanerAction(_sudokuProvider, _gridHistoryManager);
        }
    }
}
