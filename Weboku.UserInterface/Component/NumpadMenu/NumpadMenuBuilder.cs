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
        private readonly NumpadMenuProvider _numpadMenuProvider;
        private readonly DomainFacade _domainFacade;
        private readonly HotkeyProvider _hotkeyProvider;

        public NumpadMenuBuilder(
            NumpadMenuProvider numpadMenuProvider,
            DomainFacade domainFacade,
            HotkeyProvider hotkeyProvider
        )
        {
            _numpadMenuProvider = numpadMenuProvider;
            _domainFacade = domainFacade;
            _hotkeyProvider = hotkeyProvider;
        }

        private readonly Dictionary<int, NumpadMenuLabel> _dict = new();

        public NumpadMenuLabel SelectValue(int value)
        {
            if (!_dict.ContainsKey(value))
            {
                var selectValue = new RelayCommand
                (
                    () => _domainFacade.SelectValue(value),
                    () => _domainFacade.CanUseValueFilter(value)
                );

                var menuOptionSettings = new MenuOptionSettings
                {
                    Command = selectValue,
                    SelectableMenuItemContainer = _numpadMenuProvider.FilterContainer,
                    IsSelectable = true,
                    Label = value.ToString(),
                    Tooltip = "select-value__tooltip"
                };
                var command = new NumpadMenuLabel(menuOptionSettings);
                _dict[value] = command;
                _hotkeyProvider.Register(new Hotkey {Command = command, Key = value.ToString()});
            }

            return _dict[value];
        }

        public NumpadMenuLabel Redo()
        {
            var relayCommand = new RelayCommand
            (
                () => _domainFacade.Redo(),
                () => !_domainFacade.CanRedo
            );
            var menuOptionSettings = new MenuOptionSettings
            {
                Command = relayCommand,
                IsSelectable = false,
                Label = "numpad-redo__label",
                Tooltip = "numpad-redo__tooltip"
            };
            var numpadMenuLabel = new NumpadMenuLabel(menuOptionSettings);
            var hotkey = new Hotkey {Command = numpadMenuLabel, Key = "y", Ctrl = true};
            _hotkeyProvider.Register(hotkey);
            return numpadMenuLabel;
        }

        public NumpadMenuLabel Undo()
        {
            var relayCommand = new RelayCommand
            (
                () => _domainFacade.Undo(),
                () => !_domainFacade.CanUndo
            );
            var menuOptionSettings = new MenuOptionSettings
            {
                Command = relayCommand,
                IsSelectable = false,
                Label = "numpad-undo__label",
                Tooltip = "numpad-undo__tooltip"
            };
            var numpadMenuLabel = new NumpadMenuLabel(menuOptionSettings);
            var hotkey = new Hotkey {Command = numpadMenuLabel, Key = "z", Ctrl = true};
            _hotkeyProvider.Register(hotkey);
            return numpadMenuLabel;
        }

        private NumpadMenuLabel _pairsNumpadMenuItem;

        public NumpadMenuLabel Pairs()
        {
            var pairFilter = new PairFilter();
            var relayCommand = new RelayCommand(
                () => _domainFacade.SetFilter(pairFilter),
                () => _domainFacade.CanUsePairFilter()
            );
            var menuOptionSettings = new MenuOptionSettings
            {
                Command = relayCommand,
                SelectableMenuItemContainer = _numpadMenuProvider.FilterContainer,
                IsSelectable = true,
                Label = "numpad-pairs__label",
                Tooltip = "numpad-pairs__tooltip"
            };
            var numpadMenuLabel = new NumpadMenuLabel(menuOptionSettings);
            var hotkey = new Hotkey {Command = numpadMenuLabel, Key = "x"};
            _hotkeyProvider.Register(hotkey);
            return _pairsNumpadMenuItem ??= numpadMenuLabel;
        }

        public NumpadMenuLabel ClearColors()
        {
            var relayCommand = new RelayCommand(() => _domainFacade.ClearAllCellColors());
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