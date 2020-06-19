﻿using Core.Data;
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
        private readonly FilterProvider _filterProvider;
        private readonly IClickableActionProvider _clickableActionProvider;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly CellColorProvider _cellColorProvider;
        private readonly NumpadMenuProvider _numpadMenuProvider;
        private readonly CommandProvider _commandProvider;
        private readonly IGridProvider _gridProvider;
        private readonly RedoCommand _redoCommand;
        private readonly SelectPairsFilterCommand _selectPairsFilterCommand;
        private readonly ClearColorsCommand _clearColorsCommand;
        private readonly UndoCommand _undoCommand;
        private readonly SelectCleanerAction _selectCleanerAction;
        private readonly SelectStandardActionCommand _selectStandardActionCommand;
        private readonly SelectEraserActionCommand _selectEraserActionCommand;
        private readonly SelectColorActionCommand _selectColorActionCommand;

        public NumpadMenuBuilder(
            FilterProvider filterProvider, 
            IClickableActionProvider clickableActionProvider, 
            IGridHistoryManager gridHistoryManager, 
            CellColorProvider cellColorProvider, 
            NumpadMenuProvider numpadMenuProvider,
            CommandProvider commandProvider,
            IGridProvider gridProvider,
            RedoCommand redoCommand,
            SelectPairsFilterCommand selectPairsFilterCommand,
            ClearColorsCommand clearColorsCommand,
            UndoCommand undoCommand,
            SelectCleanerAction selectCleanerAction,
            SelectStandardActionCommand selectStandardActionCommand,
            SelectEraserActionCommand selectEraserActionCommand,
            SelectColorActionCommand selectColorActionCommand
            )
        {
            _filterProvider = filterProvider;
            _clickableActionProvider = clickableActionProvider;
            _gridHistoryManager = gridHistoryManager;
            _cellColorProvider = cellColorProvider;
            _numpadMenuProvider = numpadMenuProvider;
            _commandProvider = commandProvider;
            _gridProvider = gridProvider;
            _redoCommand = redoCommand;
            _selectPairsFilterCommand = selectPairsFilterCommand;
            _clearColorsCommand = clearColorsCommand;
            _undoCommand = undoCommand;
            _selectCleanerAction = selectCleanerAction;
            _selectStandardActionCommand = selectStandardActionCommand;
            _selectEraserActionCommand = selectEraserActionCommand;
            _selectColorActionCommand = selectColorActionCommand;
        }

        readonly Dictionary<int, SelectValueNumpadMenuItem> _dict = new Dictionary<int, SelectValueNumpadMenuItem>();
        public SelectValueNumpadMenuItem SelectValue(int value)
        {
            if( !_dict.ContainsKey(value) )
            {
                _dict[value] = new SelectValueNumpadMenuItem(value, _gridProvider, _numpadMenuProvider, _commandProvider);
            }
            return _dict[value];
        }

        public RedoNumpadMenuItem Redo()
        {
            var command = new RedoNumpadMenuItem(_redoCommand, _gridHistoryManager);
            return command;
        }

        public UndoNumpadMenuItem Undo()
        {
            var command = new UndoNumpadMenuItem(_gridHistoryManager, _undoCommand);
            return command;
        }

        PairsNumpadMenuItem _pairsNumpadMenuItem;
        public PairsNumpadMenuItem Pairs()
        {
            return _pairsNumpadMenuItem ??= new PairsNumpadMenuItem(_numpadMenuProvider, _selectPairsFilterCommand, _gridProvider);
        }

        public ClearColorsNumpadMenuItem ClearColors()
        {
            var command = new ClearColorsNumpadMenuItem(_clearColorsCommand);
            return command;
        }

        public SelectColorMenuItem SelectColor(Color color1, Color color2)
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
            return _eraseMenuItem ??= new SelectCleanerActionMenuItem(_numpadMenuProvider, _selectCleanerAction); 
        }

        public SelectStandardActionMenuItem SelectStandardAction()
        {
            return new SelectStandardActionMenuItem(_numpadMenuProvider, _selectStandardActionCommand);
        }

        public SelectEraserActionMenuItem SelectEraserAction()
        {
            return new SelectEraserActionMenuItem(_numpadMenuProvider, _selectEraserActionCommand);
        }

        public SelectColorActionMenuItem SelectColorAction()
        {
            return new SelectColorActionMenuItem(_numpadMenuProvider, _selectColorActionCommand);
        }
    }
}