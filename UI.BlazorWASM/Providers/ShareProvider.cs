using Core;
using Core.Data;
using Core.Serializers;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Filters;

namespace UI.BlazorWASM.Providers
{
    public class ShareProvider : IProvider
    {
        private readonly NavigationManager _navigationManager;
        private readonly ModalProvider _modalProvider;
        private readonly FilterProvider _filterProvider;

        public event Action OnChanged;
        private readonly DomainFacade _domainFacade;
        public string Converted { get; private set; }

        private SharedConverter _sharedConverter = SharedConverter.MyLink;
        public SharedConverter SharedConverter
        {
            get => _sharedConverter;
            set
            {
                _sharedConverter = value;
                Update();
            }
        }
        private SharedFields _sharedFields = SharedFields.Everything;
        public SharedFields SharedFields
        {
            get => _sharedFields;
            set
            {
                _sharedFields = value;
                Update();
            }
        }

        public bool IsOpened { get; set; }

        public ShareProvider(
            DomainFacade domainFacade,
            NavigationManager navigationManager,
            ModalProvider modalProvider,
            FilterProvider filterProvider
            )
        {
            _domainFacade = domainFacade;
            _navigationManager = navigationManager;
            _modalProvider = modalProvider;
            _filterProvider = filterProvider;
            modalProvider.OnChanged += CheckVisibility;
            CheckVisibility();
        }

        private IFilter _previousFilter;
        private void CheckVisibility()
        {
            if( !IsOpened && _modalProvider.CurrentState == Component.Modals.ModalState.Share )
            {
                IsOpened = true;
                _previousFilter = _filterProvider.Filter;
                Update();
                OnChanged?.Invoke();
            }
            else if( IsOpened && _modalProvider.CurrentState != Component.Modals.ModalState.Share )
            {
                IsOpened = false;
                _filterProvider.SetFilter(_previousFilter);
                OnChanged?.Invoke();
            }
        }

        private static IGrid TransformGrid(IGrid input, SharedFields sharedFields)
        {
            var output = input.Clone();
            if( sharedFields == SharedFields.Everything ) return output;

            output.ClearAllCandidates();
            if( sharedFields == SharedFields.GivensAndInputs ) return output;

            foreach( var position in Position.Positions.Where(position => !output.GetIsGiven(position)) )
            {
                output.SetValue(position, Value.None);
            }
            return output;
        }

        private static string SerializeGridToShareableFormat(IGrid grid, SharedConverter sharedConverter, NavigationManager navigationManager)
        {
            var gridSerializer = sharedConverter switch
            {
                SharedConverter.Hodoku => GridSerializerFactory.Make(GridSerializerName.Hodoku),
                SharedConverter.MyFormat => GridSerializerFactory.Make(GridSerializerName.Base64),
                SharedConverter.MyLink => GridSerializerFactory.Make(GridSerializerName.Base64),
                _ => throw new ArgumentException($"Incorrect option: {nameof(sharedConverter)} = {sharedConverter}")
            };

            var serialized = gridSerializer.Serialize(grid);
            return sharedConverter == SharedConverter.MyLink
                ? $"{navigationManager.BaseUri}paste/{serialized}"
                : serialized;
        }

        private void Update()
        {
            var grid = TransformGrid(_domainFacade.Grid, _sharedFields);
            Converted = SerializeGridToShareableFormat(grid, SharedConverter, _navigationManager);

            _filterProvider.SetFilter(new SharedFilter(this));
            OnChanged?.Invoke();
        }
    }
}
