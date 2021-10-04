using System;
using System.Collections.Generic;
using Weboku.Application.Enums;

namespace Weboku.Application.Managers
{
    public sealed class ModalStateManager
    {
        public ModalStateManager()
        {
            _previousStates = new();
            _previousStates.Push(ModalState.None);
        }

        private readonly Stack<ModalState> _previousStates;

        public ModalState CurrentModalState { get; private set; }

        public void SetModalState(ModalState modalState)
        {
            if (CurrentModalState == modalState) return;
            _previousStates.Push(CurrentModalState);
            CurrentModalState = modalState;
            ModalStateChanged();
        }

        private void ModalStateChanged()
        {
            OnModalStateChanged?.Invoke();
        }

        public void GoToPreviousModalState()
        {
            if (HasPreviousModalState)
            {
                CurrentModalState = _previousStates.Pop();
                ModalStateChanged();
            }
        }

        public bool HasPreviousModalState
        {
            get => _previousStates.Count > 0;
        }

        public event Action OnModalStateChanged;
    }
}