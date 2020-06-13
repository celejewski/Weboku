using Core.Converters;
using Core.Data;
using Microsoft.AspNetCore.Components;
using System;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Providers
{
    public class ShareProvider : IProvider
    {
        private bool _dirty;

        private readonly Base64GridConverter _base64GridConverter;
        private readonly NavigationManager _navigationManager;
        public event Action OnChanged;
        private readonly IGridProvider _gridProvider;
        private readonly HodokuGridConverter _hodokuGridConverter;

        private Func<int, int, InputValue> CellToValue
        {
            get
            {
                return _sharedFields switch
                {
                    SharedFields.Givens => (int x, int y) =>  _gridProvider.GetIsGiven(x, y) ? _gridProvider.GetValue(x, y) : InputValue.Empty,
                    SharedFields.GivensAndInputs => (int x, int y) => _gridProvider.GetValue(x, y),
                    _ => throw new NotImplementedException(),
                };
            }
        }

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
        private SharedFields _sharedFields = SharedFields.GivensAndInputs;
        public SharedFields SharedFields 
        { 
            get =>_sharedFields;
            set
            {
                _sharedFields = value;
                Update();
            }
        }


        private readonly IGridV2 _grid = new GridV2();
        public IGridV2 Grid
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
            IGridProvider gridProvider,
            HodokuGridConverter hodokuGridConverter,
            Base64GridConverter base64GridConverter,
            NavigationManager navigationManager
            )
        {
            _dirty = true;
            _gridProvider = gridProvider;
            _hodokuGridConverter = hodokuGridConverter;
            _base64GridConverter = base64GridConverter;
            _navigationManager = navigationManager;
            gridProvider.OnValueOrCandidatesChanged += () => _dirty = true;
        }

        private void Update()
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    _grid.SetValue(x, y, (InputValue) CellToValue(x, y));
                }
            }

            IGridConverter converter = SharedConverter switch
            {
                SharedConverter.Hodoku => _hodokuGridConverter,
                SharedConverter.MyFormat => _base64GridConverter,
                SharedConverter.MyLink => _base64GridConverter,
                _ => throw new ArgumentException("incorrect SharedConverter")
            };
            var text = converter.ToText(_grid);
            if( SharedConverter == SharedConverter.MyLink )
            {
                _converted = $"{_navigationManager.BaseUri}paste/{text}";
            }
            else
            {
                _converted = text;
            }
            _dirty = false;
            OnChanged?.Invoke();
        }
    }
}
