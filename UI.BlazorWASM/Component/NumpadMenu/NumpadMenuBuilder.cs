using System.Collections.Generic;
using Weboku.Application;
using Weboku.Application.Enums;
using Weboku.UserInterface.Commands;
using Weboku.UserInterface.Component.NumpadMenu.NumpadMenuOptions;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Component.NumpadMenu
{
    public class NumpadMenuBuilder
    {
        private readonly DomainFacade _gridHistoryManager;
        private readonly NumpadMenuProvider _numpadMenuProvider;
        private readonly CommandProvider _commandProvider;
        private readonly DomainFacade _domainFacade;
        private readonly RedoCommand _redoCommand;
        private readonly SelectPairsFilterCommand _selectPairsFilterCommand;
        private readonly ClearColorsCommand _clearColorsCommand;
        private readonly UndoCommand _undoCommand;
        private readonly HotkeyProvider _hotkeyProvider;

        public NumpadMenuBuilder(
            DomainFacade gridHistoryManager,
            NumpadMenuProvider numpadMenuProvider,
            CommandProvider commandProvider,
            DomainFacade domainFacade,
            RedoCommand redoCommand,
            SelectPairsFilterCommand selectPairsFilterCommand,
            ClearColorsCommand clearColorsCommand,
            UndoCommand undoCommand,
            HotkeyProvider hotkeyProvider
        )
        {
            _gridHistoryManager = gridHistoryManager;
            _numpadMenuProvider = numpadMenuProvider;
            _commandProvider = commandProvider;
            _domainFacade = domainFacade;
            _redoCommand = redoCommand;
            _selectPairsFilterCommand = selectPairsFilterCommand;
            _clearColorsCommand = clearColorsCommand;
            _undoCommand = undoCommand;
            _hotkeyProvider = hotkeyProvider;
        }

        private readonly Dictionary<int, SelectValueMenuItem> _dict = new Dictionary<int, SelectValueMenuItem>();

        public SelectValueMenuItem SelectValue(int value)
        {
            if (!_dict.ContainsKey(value))
            {
                var command = new SelectValueMenuItem(value, _domainFacade, _numpadMenuProvider, _commandProvider);
                _dict[value] = command;
                _hotkeyProvider.Register(new Hotkey {Command = command, Key = value.ToString()});
            }

            return _dict[value];
        }

        public RedoNumpadMenuItem Redo()
        {
            var command = new RedoNumpadMenuItem(_redoCommand, _gridHistoryManager);
            _hotkeyProvider.Register(new Hotkey {Command = command, Key = "y", Ctrl = true});
            return command;
        }

        public UndoMenuItem Undo()
        {
            var command = new UndoMenuItem(_gridHistoryManager, _undoCommand);
            _hotkeyProvider.Register(new Hotkey {Command = command, Key = "z", Ctrl = true});
            return command;
        }

        private PairsFilterMenuItem _pairsNumpadMenuItem;

        public PairsFilterMenuItem Pairs()
        {
            var command = new PairsFilterMenuItem(_numpadMenuProvider, _selectPairsFilterCommand, _domainFacade);
            _hotkeyProvider.Register(new Hotkey {Command = command, Key = "x"});
            return _pairsNumpadMenuItem ??= command;
        }

        public ClearColorsMenuItem ClearColors()
        {
            var command = new ClearColorsMenuItem(_clearColorsCommand);
            _hotkeyProvider.Register(new Hotkey {Command = command, Key = "r"});
            return command;
        }

        public SelectColorMenuItem SelectColor(Color color1, Color color2)
        {
            var command = new SelectColorMenuItem(color1, color2, _domainFacade, _numpadMenuProvider);
            return command;
        }

        public PlaceHolderMenuItem PlaceHolder()
        {
            return new PlaceHolderMenuItem();
        }

        private SelectActionEraserMenuItem _eraseMenuItem;


        private RelayCommand MakeSelectToolRelayCommand(Tool tool)
        {
            var relayCommand = new RelayCommand(() => _domainFacade.SelectTool(tool));
            return relayCommand;
        }

        public SelectActionEraserMenuItem SelectCleanerAction()
        {
            var relayCommand = MakeSelectToolRelayCommand(Tool.Eraser);
            return _eraseMenuItem ??= new SelectActionEraserMenuItem(_numpadMenuProvider, relayCommand);
        }

        public SelectActionMarkerMenuItem SelectStandardAction()
        {
            var relayCommand = MakeSelectToolRelayCommand(Tool.Marker);
            return new SelectActionMarkerMenuItem(_numpadMenuProvider, relayCommand);
        }

        public SelectActionPencilMenuItem SelectEraserAction()
        {
            var relayCommand = MakeSelectToolRelayCommand(Tool.Pencil);
            return new SelectActionPencilMenuItem(_numpadMenuProvider, relayCommand);
        }

        public SelectActionBrushMenuItem SelectColorAction()
        {
            var relayCommand = MakeSelectToolRelayCommand(Tool.Brush);
            return new SelectActionBrushMenuItem(_numpadMenuProvider, relayCommand);
        }
    }
}