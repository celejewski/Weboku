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
        private readonly ISudokuProvider _sudokuProvider;
        private readonly HodokuGridConverter _hodokuGridConverter;

        private Func<ICell, int> CellToValue
        {
            get
            {
                return _sharedFields switch
                {
                    SharedFields.Givens => cell => cell.IsGiven ? cell.Input.Value : 0,
                    SharedFields.GivensAndInputs => cell => cell.Input.Value,
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


        private readonly IGrid _grid;
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
            ISudokuProvider sudokuProvider,
            HodokuGridConverter hodokuGridConverter,
            Base64GridConverter base64GridConverter,
            NavigationManager navigationManager
            )
        {
            _sudokuProvider = sudokuProvider;
            _hodokuGridConverter = hodokuGridConverter;
            _base64GridConverter = base64GridConverter;
            _navigationManager = navigationManager;
            _grid = sudokuProvider.GetGridClone();
            sudokuProvider.OnValueOrCandidatesChanged += () => _dirty = true;
        }

        private void Update()
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    var source = _sudokuProvider.Cells[x, y];
                    _grid.SetValue(x, y, CellToValue(source));
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
