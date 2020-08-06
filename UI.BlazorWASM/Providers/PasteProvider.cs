using Core.Serializer;
using Core.Data;
using System;

namespace UI.BlazorWASM.Providers
{
    public class PasteProvider : IProvider
    {
        private readonly IGridSerializer _converter;
        private readonly ModalProvider _modalProvider;
        private readonly IGridProvider _gridProvider;
        private readonly PreserveStateProvider _preserveStateProvider;

        public bool IsValidText { get; private set; }
        private IGrid _gridBackup;
        public IGrid Grid { get; private set; }
        private string _pasted;
        public string Pasted
        {
            get => _pasted;
            set
            {
                _pasted = value;
                if( IsValid )
                {
                    Grid = _converter.Deserialize(_pasted);
                    if( _isVisible )
                    {
                        _gridProvider.Grid = Grid;
                    }
                }
                OnChanged?.Invoke();
            }
        }

        public bool IsValid => IsValidText = _converter.IsValidFormat(_pasted);

        public PasteProvider(
            ChainGridSerializer chainGridConverter,
            ModalProvider modalProvider,
            IGridProvider gridProvider,
            PreserveStateProvider preserveStateProvider)
        {
            _converter = chainGridConverter;
            _modalProvider = modalProvider;
            _gridProvider = gridProvider;
            _preserveStateProvider = preserveStateProvider;
            Pasted = new string('0', 81);

            _modalProvider.OnChanged += OnModalChange;
            OnModalChange();
        }


        private bool _isVisible;
        public void OnModalChange()
        {
            if( !_isVisible && _modalProvider.CurrentState == Component.Modals.ModalState.Paste )
            {
                _isVisible = true;
                OnShow();
            }
            else if( _isVisible && _modalProvider.CurrentState != Component.Modals.ModalState.Paste )
            {
                _isVisible = false;
                OnHide();
            }
        }

        public void OnShow()
        {
            _preserveStateProvider.PauseAutoSave();
            _gridBackup = _gridProvider.Grid;
            _gridProvider.Grid = Grid;
        }

        public void OnHide()
        {
            _gridProvider.Grid = _gridBackup;
            _preserveStateProvider.ResumeAutoSave();
        }

        public event Action OnChanged;
    }
}
