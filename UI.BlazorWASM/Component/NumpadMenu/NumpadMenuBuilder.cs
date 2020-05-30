using Core.Data;
using System.Collections.Generic;
using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Component.NumpadMenu;
using UI.BlazorWASM.Component.NumpadMenu.NumpadMenuOptions;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ViewModels
{
    public class NumpadMenuBuilder
    {
        private readonly IFilterProvider _filterProvider;
        private readonly IClickableActionProvider _clickableActionProvider;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly ICellColorProvider _cellColorProvider;
        private readonly ISudokuProvider _sudokuProvider;
        private readonly NumpadMenuProvider _numpadMenuProvider;
        private readonly CommandProvider _commandProvider;

        public NumpadMenuBuilder(
            IFilterProvider filterProvider, 
            IClickableActionProvider clickableActionProvider, 
            IGridHistoryManager gridHistoryManager, 
            ICellColorProvider cellColorProvider, 
            ISudokuProvider sudokuProvider, 
            NumpadMenuProvider numpadMenuProvider,
            CommandProvider commandProvider
            )
        {
            _filterProvider = filterProvider;
            _clickableActionProvider = clickableActionProvider;
            _gridHistoryManager = gridHistoryManager;
            _cellColorProvider = cellColorProvider;
            _sudokuProvider = sudokuProvider;
            _numpadMenuProvider = numpadMenuProvider;
            _commandProvider = commandProvider;
        }

        readonly Dictionary<int, SelectValueNumpadMenuItem> _dict = new Dictionary<int, SelectValueNumpadMenuItem>();
        public SelectValueNumpadMenuItem SelectValue(int value)
        {
            if( !_dict.ContainsKey(value) )
            {
                _dict[value] = new SelectValueNumpadMenuItem(value, _sudokuProvider, _numpadMenuProvider, _commandProvider);
            }
            return _dict[value];
        }

        public RedoNumpadMenuItem Redo()
        {
            var command = new RedoNumpadMenuItem(_commandProvider, _gridHistoryManager);
            return command;
        }

        public UndoNumpadMenuItem Undo()
        {
            var command = new UndoNumpadMenuItem(_gridHistoryManager, _commandProvider);
            return command;
        }

        PairsNumpadMenuItem _pairsNumpadMenuItem;
        public PairsNumpadMenuItem Pairs()
        {
            if( _pairsNumpadMenuItem == null )
            {
                _pairsNumpadMenuItem = new PairsNumpadMenuItem(_sudokuProvider, _numpadMenuProvider, _commandProvider);
            }
            return _pairsNumpadMenuItem;
        }

        public ClearColorsNumpadMenuItem ClearColors()
        {
            var command = new ClearColorsNumpadMenuItem(_commandProvider);
            return command;
        }

        public SelectColorMenuItem SelectColor(CellColor color1, CellColor color2)
        {
            var command = new SelectColorMenuItem(color1, color2, _clickableActionProvider, _numpadMenuProvider);
            return command;
        }

        public PlaceHolderNumpadMenuItem PlaceHolder()
        {
            return new PlaceHolderNumpadMenuItem();
        }


        SelectCleanerActionMenuItem _eraseMenuItem;
        public SelectCleanerActionMenuItem SelectCleanerAction()
        {
            if( _eraseMenuItem == null )
            {
                _eraseMenuItem = new SelectCleanerActionMenuItem(_numpadMenuProvider, _commandProvider);
            }
            return _eraseMenuItem;
        }

        public SelectStandardActionMenuItem SelectStandardAction()
        {
            return new SelectStandardActionMenuItem(_numpadMenuProvider, _commandProvider);
        }

        public SelectEraserActionMenuItem SelectEraserAction()
        {
            return new SelectEraserActionMenuItem(_numpadMenuProvider, _commandProvider);
        }

        public SelectColorActionMenuItem SelectColorAction()
        {
            return new SelectColorActionMenuItem(_numpadMenuProvider, _commandProvider);
        }
    }
}
