using System;
using UI.BlazorWASM.Component.Modals;

namespace UI.BlazorWASM.Providers
{
    public class ModalProvider : IProvider
    {
        public ModalState Modal { get; private set; }

        public void SetModalState(ModalState state)
        {
            Modal = state;
            OnChanged?.Invoke();
        }

        public event Action OnChanged;
    }
}
