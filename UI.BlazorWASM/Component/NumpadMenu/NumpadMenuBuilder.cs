using System.Collections.Generic;
using Weboku.Application;
using Weboku.Application.Enums;
using Weboku.Application.Filters;
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
        private readonly HotkeyProvider _hotkeyProvider;

        public NumpadMenuBuilder(
            DomainFacade gridHistoryManager,
            NumpadMenuProvider numpadMenuProvider,
            CommandProvider commandProvider,
            DomainFacade domainFacade,
            HotkeyProvider hotkeyProvider
        )
        {
            _gridHistoryManager = gridHistoryManager;
            _numpadMenuProvider = numpadMenuProvider;
            _commandProvider = commandProvider;
            _domainFacade = domainFacade;
            _hotkeyProvider = hotkeyProvider;
        }

        private readonly Dictionary<int, SelectValueMenuItem> _dict = new Dictionary<int, SelectValueMenuItem>();

        public SelectValueMenuItem SelectValue(int value)
        {
            if (!_dict.ContainsKey(value))
            {
                var menuOptionSettings = new MenuOptionSettings
                {
                    Command = _commandProvider.SelectValue(value),
                    SelectableMenuItemContainer = _numpadMenuProvider.FilterContainer
                };
                var command = new SelectValueMenuItem(value, _domainFacade, menuOptionSettings);
                _dict[value] = command;
                _hotkeyProvider.Register(new Hotkey {Command = command, Key = value.ToString()});
            }

            return _dict[value];
        }

        public RedoNumpadMenuItem Redo()
        {
            var relayCommand = new RelayCommand(() => _domainFacade.Redo());
            var menuOptionSettings = new MenuOptionSettings {Command = relayCommand};
            var command = new RedoNumpadMenuItem(menuOptionSettings, _gridHistoryManager);
            _hotkeyProvider.Register(new Hotkey {Command = command, Key = "y", Ctrl = true});
            return command;
        }

        public UndoMenuItem Undo()
        {
            var relayCommand = new RelayCommand(() => _domainFacade.Undo());
            var menuOptionSettings = new MenuOptionSettings {Command = relayCommand};
            var command = new UndoMenuItem(menuOptionSettings, _gridHistoryManager);
            _hotkeyProvider.Register(new Hotkey {Command = command, Key = "z", Ctrl = true});
            return command;
        }

        private PairsFilterMenuItem _pairsNumpadMenuItem;

        public PairsFilterMenuItem Pairs()
        {
            var pairFilter = new PairFilter();
            var relayCommand = new RelayCommand(() => _domainFacade.SetFilter(pairFilter));
            var menuOptionSettings = new MenuOptionSettings
            {
                Command = relayCommand,
                SelectableMenuItemContainer = _numpadMenuProvider.FilterContainer
            };
            var command = new PairsFilterMenuItem(menuOptionSettings, _domainFacade);
            _hotkeyProvider.Register(new Hotkey {Command = command, Key = "x"});
            return _pairsNumpadMenuItem ??= command;
        }

        public NumpadMenuLabel ClearColors()
        {
            var relayCommand = new RelayCommand(() => _domainFacade.ClearAllColors());
            var menuOptionSettings = new MenuOptionSettings
            {
                Command = relayCommand,
                IsSelectable = false,
                Label = "numpad-clear-colors__label",
                Tooltip = "numpad-clear-colors__tooltip"
            };
            var command = new NumpadMenuLabel(menuOptionSettings);
            _hotkeyProvider.Register(new Hotkey {Command = command, Key = "r"});
            return command;
        }

        public SelectColorMenuItem SelectColor(Color color1, Color color2)
        {
            var command = new SelectColorMenuItem(color1, color2, _domainFacade, _numpadMenuProvider);
            return command;
        }

        public NumpadMenuLabel PlaceHolder()
        {
            var menuOptionSettings = new MenuOptionSettings
            {
                Command = new RelayCommand(() => { }),
                Tooltip = "",
                IsSelectable = false,
                Label = string.Empty,
            };
            return new NumpadMenuLabel(menuOptionSettings);
        }

        private NumpadMenuLabel _eraseMenuItem;


        private RelayCommand MakeSelectToolRelayCommand(Tool tool)
        {
            var relayCommand = new RelayCommand(() => _domainFacade.SelectTool(tool));
            return relayCommand;
        }

        public NumpadMenuLabel SelectCleanerAction()
        {
            var relayCommand = MakeSelectToolRelayCommand(Tool.Eraser);
            var menuOptionSettings = new MenuOptionSettings
            {
                Command = relayCommand,
                SelectableMenuItemContainer = _numpadMenuProvider.ActionContainer,
                Label = "fas fa-eraser",
                Tooltip = "select-action-eraser__tooltip",
                IsSelectable = true
            };
            return new NumpadMenuLabel(menuOptionSettings);
        }

        public NumpadMenuLabel SelectStandardAction()
        {
            var relayCommand = MakeSelectToolRelayCommand(Tool.Marker);

            var menuOptionSettings = new MenuOptionSettings
            {
                Command = relayCommand,
                SelectableMenuItemContainer = _numpadMenuProvider.ActionContainer,
                Label = "fas fa-marker",
                Tooltip = "select-action-marker__tooltip",
                IsSelectable = true
            };
            return new NumpadMenuLabel(menuOptionSettings);
        }

        public NumpadMenuLabel SelectEraserAction()
        {
            var relayCommand = MakeSelectToolRelayCommand(Tool.Pencil);
            var menuOptionSettings = new MenuOptionSettings
            {
                Command = relayCommand,
                SelectableMenuItemContainer = _numpadMenuProvider.ActionContainer,
                Label = "fas fa-pencil-alt",
                Tooltip = "select-action-pencil__tooltip",
                IsSelectable = true
            };
            return new NumpadMenuLabel(menuOptionSettings);
        }

        public NumpadMenuLabel SelectColorAction()
        {
            var relayCommand = MakeSelectToolRelayCommand(Tool.Brush);
            var menuOptionSettings = new MenuOptionSettings
            {
                Command = relayCommand,
                SelectableMenuItemContainer = _numpadMenuProvider.ActionContainer,
                Label = "fas fa-paint-roller",
                Tooltip = "select-action-brush__tooltip",
                IsSelectable = true
            };
            return new NumpadMenuLabel(menuOptionSettings);
        }
    }
}