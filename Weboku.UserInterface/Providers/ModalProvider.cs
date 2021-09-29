using System;
using System.Collections.Generic;
using Weboku.Application;
using Weboku.Application.Enums;

namespace Weboku.UserInterface.Providers
{
    public class ModalProvider : IProvider
    {
        private readonly Stack<ModalState> _previousStates = new Stack<ModalState>();
        private readonly DomainFacade _domainFacade;

        public ModalState CurrentState
        {
            get => _domainFacade.ModalState;
            private set => _domainFacade.ModalState = value;
        }

        public ModalProvider(DomainFacade domainFacade)
        {
            _previousStates.Push(ModalState.None);
            _domainFacade = domainFacade;
            _domainFacade.ModalState = ModalState.Loading;
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

            if (CurrentState == ModalState.None) _domainFacade.Unpause();
            else _domainFacade.Pause();
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