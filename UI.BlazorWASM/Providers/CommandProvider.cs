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
        private readonly IHotkeyProvider _hotkeyProvider;
        private readonly IGameTimerProvider _gameTimerProvider;

        public CommandProvider(IGridGenerator gridGenerator, IGridHistoryManager gridHistoryManager, ISudokuProvider sudokuProvider, HotkeyProvider hotkeyProvider, IGameTimerProvider gameTimerProvider)
        {
            _gridGenerator = gridGenerator;
            _gridHistoryManager = gridHistoryManager;
            _sudokuProvider = sudokuProvider;
            _hotkeyProvider = hotkeyProvider;
            _gameTimerProvider = gameTimerProvider;
        }
        public ICommand StartNewGame(string difficulty)
        {
            return new StartNewGameCommand(difficulty, _gridGenerator, _gridHistoryManager, _sudokuProvider, _gameTimerProvider);
        }

        public ICommand FindAllCandidates()
        {
            var command = new FindAllCandidatesCommand(_sudokuProvider, _gridHistoryManager);
            _hotkeyProvider.Register(new Hotkey { Command = command, Key = "f", Ctrl = true });
            return command;
        }

        public ICommand Restart()
        {
            return new RestartCommand(_sudokuProvider, _gridHistoryManager);
        }
    }
}
