﻿using Core.Generators;
using Core.Serializer;
using System.Threading.Tasks;
using UI.BlazorWASM.Component.Modals;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class StartNewGameCommand : ICommand
    {
        private readonly string _difficulty;
        private readonly ISudokuGenerator _generator;
        private readonly IGridProvider _gridProvider;
        private readonly ModalProvider _modalProvider;
        private readonly CellColorProvider _cellColorProvider;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly GameTimerProvider _gameTimerProvider;
        private readonly IGridSerializer _hodokuGridConverter;
        private readonly SudokuProvider _sudokuProvider;

        public StartNewGameCommand(
            string difficulty,
            ISudokuGenerator generator,
            IGridProvider gridProvider,
            ModalProvider modalProvider,
            CellColorProvider cellColorProvider,
            IGridHistoryManager gridHistoryManager,
            GameTimerProvider gameTimerProvider,
            SudokuProvider sudokuProvider)
        {
            _difficulty = difficulty;
            _generator = generator;
            _gridProvider = gridProvider;
            _modalProvider = modalProvider;
            _cellColorProvider = cellColorProvider;
            _gridHistoryManager = gridHistoryManager;
            _gameTimerProvider = gameTimerProvider;
            _hodokuGridConverter = GridSerializerFactory.Make(GridSerializerName.Hodoku);
            _sudokuProvider = sudokuProvider;
        }

        public async Task Execute()
        {
            var sudoku = await _generator.Generate(_difficulty);
            _gridProvider.Grid = _hodokuGridConverter.Deserialize(sudoku.Given);
            _sudokuProvider.PreferredDifficulty = _difficulty;
            _sudokuProvider.Sudoku = sudoku;
            _modalProvider.SetModalState(ModalState.None);
            _cellColorProvider.ClearAll();
            _gridHistoryManager.ClearUndo();
            _gameTimerProvider.Start();
        }
    }
}
