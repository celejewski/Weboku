using System;
using System.Collections.Generic;
using Weboku.Application.Enums;

namespace Weboku.Application
{
    public sealed partial class DomainFacade
    {
        private readonly Stack<ModalState> _previousStates;

        public ModalState CurrentState
        {
            get => ModalState;
            private set => ModalState = value;
        }


        public void SetModalState(ModalState state)
        {
            _previousStates.Push(CurrentState);
            CurrentState = state;
            ModalStateChanged();
        }

        private void ModalStateChanged()
        {
            OnChanged?.Invoke();

            if (CurrentState == ModalState.None) Unpause();
            else Pause();
        }

        public void GoToPreviousState()
        {
            if (HasPreviousState)
            {
                CurrentState = _previousStates.Pop();
                ModalStateChanged();
            }
        }

        public bool HasPreviousState
        {
            get => _previousStates.Count > 0;
        }

        public event Action OnChanged;
    }
}