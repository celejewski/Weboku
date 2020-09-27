using Application;
using Application.Data;
using System;
using UI.BlazorWASM.Filters;

namespace UI.BlazorWASM.Providers
{
    public class ShareProvider : IProvider
    {
        private readonly ModalProvider _modalProvider;
        private readonly FilterProvider _filterProvider;

        public event Action OnChanged;
        private readonly DomainFacade _domainFacade;
        public string Converted { get => _domainFacade.SharedOutput; }

        public SharedConverter SharedConverter
        {
            get => _domainFacade.SharedConverter;
            set
            {
                _domainFacade.SharedConverter = value;
                Update();
            }
        }
        public SharedFields SharedFields
        {
            get => _domainFacade.SharedFields;
            set
            {
                _domainFacade.SharedFields = value;
                Update();
            }
        }

        public bool IsOpened { get; set; }

        public ShareProvider(
            DomainFacade domainFacade,
            ModalProvider modalProvider,
            FilterProvider filterProvider
            )
        {
            _domainFacade = domainFacade;
            _modalProvider = modalProvider;
            _filterProvider = filterProvider;
            modalProvider.OnChanged += CheckVisibility;
            CheckVisibility();
        }

        private IFilter _previousFilter;
        private void CheckVisibility()
        {
            if( !IsOpened && _modalProvider.CurrentState == Application.Enums.ModalState.Share )
            {
                IsOpened = true;
                _previousFilter = _filterProvider.Filter;
                Update();
                OnChanged?.Invoke();
            }
            else if( IsOpened && _modalProvider.CurrentState != Application.Enums.ModalState.Share )
            {
                IsOpened = false;
                _filterProvider.SetFilter(_previousFilter);
                OnChanged?.Invoke();
            }
        }

        private void Update()
        {
            _filterProvider.SetFilter(new SharedFilter(this));
            OnChanged?.Invoke();
        }
    }
}
