using Core;
using System.Collections.Generic;
using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Component.NumpadMenu;
using UI.BlazorWASM.Component.NumpadMenu.NumpadMenuOptions;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.ViewModels
{
    public class NumpadMenuBuilder
    {
        private readonly IClickableActionProvider _clickableActionProvider;
        private readonly DomainFacade _gridHistoryManager;
        private readonly NumpadMenuProvider _numpadMenuProvider;
        private readonly CommandProvider _commandProvider;
        private readonly DomainFacade _gridProvider;
        private readonly RedoCommand _redoCommand;
        private readonly SelectPairsFilterCommand _selectPairsFilterCommand;
        private readonly ClearColorsCommand _clearColorsCommand;
        private readonly UndoCommand _undoCommand;
        private readonly SelectActionEraserCommand _selectActionEraserCommand;
        private readonly SelectActionMarkerCommand _selectActionMarkerCommand;
        private readonly SelectActionPencilCommand _selectActionPencilCommand;
        private readonly SelectActionBrushCommand _selectActionBrushCommand;
        private readonly HotkeyProvider _hotkeyProvider;

        public NumpadMenuBuilder(
            IClickableActionProvider clickableActionProvider,
            DomainFacade gridHistoryManager,
            NumpadMenuProvider numpadMenuProvider,
            CommandProvider commandProvider,
            DomainFacade gridProvider,
            RedoCommand redoCommand,
            SelectPairsFilterCommand selectPairsFilterCommand,
            ClearColorsCommand clearColorsCommand,
            UndoCommand undoCommand,
            SelectActionEraserCommand selectCleanerAction,
            SelectActionMarkerCommand selectStandardActionCommand,
            SelectActionPencilCommand selectEraserActionCommand,
            SelectActionBrushCommand selectColorActionCommand,
            HotkeyProvider hotkeyProvider
            )
        {
            _clickableActionProvider = clickableActionProvider;
            _gridHistoryManager = gridHistoryManager;
            _numpadMenuProvider = numpadMenuProvider;
            _commandProvider = commandProvider;
            _gridProvider = gridProvider;
            _redoCommand = redoCommand;
            _selectPairsFilterCommand = selectPairsFilterCommand;
            _clearColorsCommand = clearColorsCommand;
            _undoCommand = undoCommand;
            _selectActionEraserCommand = selectCleanerAction;
            _selectActionMarkerCommand = selectStandardActionCommand;
            _selectActionPencilCommand = selectEraserActionCommand;
            _selectActionBrushCommand = selectColorActionCommand;
            _hotkeyProvider = hotkeyProvider;




        }

        private readonly Dictionary<int, SelectValueMenuItem> _dict = new Dictionary<int, SelectValueMenuItem>();
        public SelectValueMenuItem SelectValue(int value)
        {
            if( !_dict.ContainsKey(value) )
            {
                var command = new SelectValueMenuItem(value, _gridProvider, _numpadMenuProvider, _commandProvider);
                _dict[value] = command;
                _hotkeyProvider.Register(new Hotkey { Command = command, Key = value.ToString() });
            }
            return _dict[value];
        }

        public RedoNumpadMenuItem Redo()
        {
            var command = new RedoNumpadMenuItem(_redoCommand, _gridHistoryManager);
            _hotkeyProvider.Register(new Hotkey { Command = command, Key = "y", Ctrl = true });
            return command;
        }

        public UndoMenuItem Undo()
        {
            var command = new UndoMenuItem(_gridHistoryManager, _undoCommand);
            _hotkeyProvider.Register(new Hotkey { Command = command, Key = "z", Ctrl = true });
            return command;
        }

        private PairsFilterMenuItem _pairsNumpadMenuItem;
        public PairsFilterMenuItem Pairs()
        {
            var command = new PairsFilterMenuItem(_numpadMenuProvider, _selectPairsFilterCommand, _gridProvider);
            _hotkeyProvider.Register(new Hotkey { Command = command, Key = "x" });
            return _pairsNumpadMenuItem ??= command;
        }

        public ClearColorsMenuItem ClearColors()
        {
            var command = new ClearColorsMenuItem(_clearColorsCommand);
            _hotkeyProvider.Register(new Hotkey { Command = command, Key = "r" });
            return command;
        }

        public SelectColorMenuItem SelectColor(Color color1, Color color2)
        {
            var command = new SelectColorMenuItem(color1, color2, _clickableActionProvider, _numpadMenuProvider);
            return command;
        }

        public PlaceHolderMenuItem PlaceHolder()
        {
            return new PlaceHolderMenuItem();
        }

        private SelectActionEraserMenuItem _eraseMenuItem;
        public SelectActionEraserMenuItem SelectCleanerAction()
        {
            return _eraseMenuItem ??= new SelectActionEraserMenuItem(_numpadMenuProvider, _selectActionEraserCommand);
        }

        public SelectActionMarkerMenuItem SelectStandardAction()
        {
            return new SelectActionMarkerMenuItem(_numpadMenuProvider, _selectActionMarkerCommand);
        }

        public SelectActionPencilMenuItem SelectEraserAction()
        {
            return new SelectActionPencilMenuItem(_numpadMenuProvider, _selectActionPencilCommand);
        }

        public SelectActionBrushMenuItem SelectColorAction()
        {
            return new SelectActionBrushMenuItem(_numpadMenuProvider, _selectActionBrushCommand);
        }
    }
}
