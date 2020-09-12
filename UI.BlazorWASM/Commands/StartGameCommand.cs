﻿using System.Threading.Tasks;
using UI.BlazorWASM.Component.Modals;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class StartGameCommand
    {

        private readonly ModalProvider _modalProvider;
        private readonly CellColorProvider _cellColorProvider;
        private readonly GridHistoryProvider _gridHistoryManager;
        private readonly GameTimerProvider _gameTimerProvider;

        public StartGameCommand(
            ModalProvider modalProvider,
            CellColorProvider cellColorProvider,
            GridHistoryProvider gridHistoryManager,
            GameTimerProvider gameTimerProvider)
        {
            _modalProvider = modalProvider;
            _cellColorProvider = cellColorProvider;
            _gridHistoryManager = gridHistoryManager;
            _gameTimerProvider = gameTimerProvider;
        }

        public async Task Execute()
        {
            _modalProvider.SetModalState(ModalState.None);
            _cellColorProvider.ClearAll();
            _gridHistoryManager.ClearUndo();
            _gameTimerProvider.Start();
        }

    }
}
