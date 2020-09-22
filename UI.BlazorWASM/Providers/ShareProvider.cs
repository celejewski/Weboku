using Core;
using Core.Data;
using Core.Serializers;
using Microsoft.AspNetCore.Components;
using System;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Filters;

namespace UI.BlazorWASM.Providers
{
    public class ShareProvider : IProvider
    {
        private bool _dirty;

        private readonly NavigationManager _navigationManager;
        private readonly ModalProvider _modalProvider;
        private readonly FilterProvider _filterProvider;

        public event Action OnChanged;
        private readonly DomainFacade _gridProvider;
        private readonly IGridSerializer _hodokuGridConverter;
        private string _converted;
        public string Converted
        {
            get
            {
                if( _dirty )
                {
                    Update();
                }
                return _converted;
            }
            private set => _converted = value;
        }

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


        private IGrid _grid = new Grid();
        public IGrid Grid
        {
            get
            {
                if( _dirty )
                {
                    Update();
                }
                return _grid;
            }
        }

        public ShareProvider(
            DomainFacade gridProvider,
            NavigationManager navigationManager,
            ModalProvider modalProvider,
            FilterProvider filterProvider
            )
        {
            _dirty = true;
            _gridProvider = gridProvider;
            _hodokuGridConverter = GridSerializerFactory.Make(GridSerializerName.Hodoku);
            _navigationManager = navigationManager;
            _modalProvider = modalProvider;
            _filterProvider = filterProvider;
            gridProvider.OnValueOrCandidateChanged += () => _dirty = true;
            modalProvider.OnChanged += () => CheckVisibility();
            CheckVisibility();
        }

        private bool _isOpened = false;
        private IFilter _previousFilter = null;
        public void CheckVisibility()
        {
            Console.WriteLine("Check visibilty");
            if( !_isOpened && _modalProvider.CurrentState == Component.Modals.ModalState.Share )
            {
                Console.WriteLine("Share is opening");
                _isOpened = true;
                _previousFilter = _filterProvider.Filter;
                _filterProvider.SetFilter(new SharedFilter(this));
            }
            else if( _isOpened && _modalProvider.CurrentState != Component.Modals.ModalState.Share )
            {
                Console.WriteLine("Share is closing");
                _isOpened = false;
                _filterProvider.SetFilter(_previousFilter);
            }
        }

        private static IGrid TransformGrid(IGrid input, SharedFields sharedFields)
        {
            var output = input.Clone();
            if( sharedFields == SharedFields.Everything )
            {
                return output;
            }

            foreach( var pos in Position.All )
            {
                if( sharedFields == SharedFields.Givens && !output.GetIsGiven(pos) )
                {
                    output.SetValue(pos, Value.None);
                }
                output.ClearCandidates(pos);
            }
            return output;
        }

        private void Update()
        {
            _grid = TransformGrid(_gridProvider.Grid, _sharedFields);
            IGridSerializer converter = SharedConverter switch
            {
                SharedConverter.Hodoku => _hodokuGridConverter,
                SharedConverter.MyFormat => GridSerializerFactory.Make(GridSerializerName.Base64),
                SharedConverter.MyLink => GridSerializerFactory.Make(GridSerializerName.Base64),
                _ => throw new ArgumentException("incorrect SharedConverter")
            };
            var text = converter.Serialize(_grid);
            if( SharedConverter == SharedConverter.MyLink )
            {
                _converted = $"{_navigationManager.BaseUri}paste/{text}";
            }
            else
            {
                _converted = text;
            }
            _dirty = false;

            _filterProvider.SetFilter(new SharedFilter(this));
            OnChanged?.Invoke();
        }
    }
}
