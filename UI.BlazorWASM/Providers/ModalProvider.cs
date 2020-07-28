using System;
using System.Collections.Generic;
using UI.BlazorWASM.Component.Modals;

namespace UI.BlazorWASM.Providers
{
    public class ModalProvider : IProvider
    {
        private readonly Stack<ModalState> _previousStates = new Stack<ModalState>();
        public ModalState CurrentState { get; private set; } = ModalState.Loading;

        public ModalProvider()
        {
            _previousStates.Push(ModalState.None);
        }

        public void SetModalState(ModalState state)
        {
            _previousStates.Push(CurrentState);
            CurrentState = state;
            OnChanged?.Invoke();
        }

        public void GoToPreviousState()
        {
            if( HasPreviousState )
            {
                CurrentState = _previousStates.Pop();
                OnChanged?.Invoke();
            }
        }

        public bool HasPreviousState
        {
            get => _previousStates.Count > 0;
        }

        public event Action OnChanged;
    }
}
