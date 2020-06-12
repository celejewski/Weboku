﻿using Core.Converters;
using Core.Generators;
using System.Threading.Tasks;
using UI.BlazorWASM.Component.Modals;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class StartNewGameCommand : ICommand
    {
        private readonly string _difficulty;
        private readonly ISudokuGenerator _sudokuGenerator;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly GameTimerProvider _gameTimerProvider;
        private readonly IGridConverter _gridConverter;
        private readonly ModalProvider _modalProvider;
        private readonly CellColorProvider _cellColorProvider;

        public StartNewGameCommand(
            string difficulty, 
            ISudokuGenerator sudokuGenerator, 
            IGridHistoryManager gridHistoryManager, 
            GameTimerProvider gameTimerProvider, 
            IGridConverter gridConverter,
            ModalProvider modalProvider,
            CellColorProvider cellColorProvider)
        {
            _difficulty = difficulty;
            _sudokuGenerator = sudokuGenerator;
            _gridHistoryManager = gridHistoryManager;
            _gameTimerProvider = gameTimerProvider;
            _gridConverter = gridConverter;
            _modalProvider = modalProvider;
            _cellColorProvider = cellColorProvider;
        }
        public async Task Execute()
        {
            var sudoku = await _sudokuGenerator.Generate(_difficulty);
            var newGrid = _gridConverter.FromText(sudoku.Given);
            _cellColorProvider.ClearAll();
            _gridHistoryManager.ClearUndo();
            _modalProvider.SetModalState(ModalState.None);
            _gameTimerProvider.Start();
        }
    }
}
