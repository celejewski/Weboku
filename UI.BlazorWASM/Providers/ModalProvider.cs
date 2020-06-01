using System;
using UI.BlazorWASM.Component.Modals;

namespace UI.BlazorWASM.Providers
{
    public class ModalProvider : IProvider
    {
        public ModalState PreviousState { get; private set; }
        public ModalState CurrentState { get; private set; }

        public void SetModalState(ModalState state)
        {
            PreviousState = CurrentState;
            CurrentState = state;
            OnChanged?.Invoke();
        }

        public event Action OnChanged;
    }
}
