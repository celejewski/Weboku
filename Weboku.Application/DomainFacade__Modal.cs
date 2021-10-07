using System;
using Weboku.Application.Enums;
using Weboku.Application.Managers;

namespace Weboku.Application
{
    public sealed partial class DomainFacade
    {
        private readonly ModalStateManager _modalStateManager;

        public ModalState CurrentModalState => _modalStateManager.CurrentModalState;

        public void SetModalState(ModalState modalState)
        {
            _modalStateManager.SetModalState(modalState);
        }


        public void GoToPreviousModalState() => _modalStateManager.GoToPreviousModalState();

        public bool HasPreviousModalState => _modalStateManager.HasPreviousModalState;

        public event Action OnModalStateChanged
        {
            add => _modalStateManager.OnModalStateChanged += value;
            remove => _modalStateManager.OnModalStateChanged -= value;
        }


        private void HandleModalStateChanged()
        {
            var modalState = _modalStateManager.CurrentModalState;
            if (modalState == ModalState.Share) _shareManager.UpdateGrid(_grid);

            GridChanged();

            if (modalState == ModalState.None) _gameTimerManager.Unpause();
            else _gameTimerManager.Pause();
        }
    }
}