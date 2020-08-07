﻿using Core.Serializer;
using System.Threading.Tasks;
using UI.BlazorWASM.Component.Modals;
using UI.BlazorWASM.Providers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class StartNewGameFromPastedCommand : ICommand
    {
        private readonly PasteProvider _pasteProvider;
        private readonly ModalProvider _modalProvider;
        private readonly CellColorProvider _cellColorProvider;
        private readonly GridHistoryProvider _gridHistoryManager;
        private readonly GameTimerProvider _gameTimerProvider;
        private readonly IGridProvider _gridProvider;
        private readonly SudokuProvider _sudokuProvider;

        public StartNewGameFromPastedCommand(
            PasteProvider pasteProvider,
            ModalProvider modalProvider,
            CellColorProvider cellColorProvider,
            GridHistoryProvider gridHistoryManager,
            GameTimerProvider gameTimerProvider,
            IGridProvider gridProvider,
            SudokuProvider sudokuProvider)
        {
            _pasteProvider = pasteProvider;
            _modalProvider = modalProvider;
            _cellColorProvider = cellColorProvider;
            _gridHistoryManager = gridHistoryManager;
            _gameTimerProvider = gameTimerProvider;
            _gridProvider = gridProvider;
            _sudokuProvider = sudokuProvider;
        }

        public Task Execute()
        {
            _modalProvider.SetModalState(ModalState.None);
            var grid = _pasteProvider.Grid.Clone();
            _gridProvider.Grid = grid;

            var converter = GridSerializerFactory.Make(GridSerializerName.Hodoku);

            var sudoku = new Core.Data.Sudoku { Given = converter.Serialize(grid) };
            _sudokuProvider.Sudoku = sudoku;

            _cellColorProvider.ClearAll();
            _gridHistoryManager.ClearUndo();
            _gameTimerProvider.Start();
            return Task.CompletedTask;
        }
    }
}
