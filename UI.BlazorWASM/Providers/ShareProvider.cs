using Core.Converters;
using Core.Data;
using System;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Providers
{
    public class ShareProvider : IProvider
    {
        private bool _dirty;
        private readonly IGridConverter _converter;
        private Func<ICell, int> _cellToValue;
        public event Action OnChanged;
        private readonly ISudokuProvider _sudokuProvider;

        private string _converted;
        public string Converted 
        { 
            get
            {
                if (_dirty)
                {
                    Update();
                }
                return _converted;
            } 
            private set => _converted = value;
        }


        private readonly IGrid _grid;
        public IGrid Grid
        {
            get 
            {
                if (_dirty)
                {
                    Update();
                }
                return _grid;
            }
        }
        public SharedFields SharedFields { get; set; } = SharedFields.GivensAndInputs;

        public ShareProvider(ISudokuProvider sudokuProvider, HodokuGridConverter hodokuGridConverter)
        {
            _sudokuProvider = sudokuProvider;
            _converter = hodokuGridConverter;
            _grid = sudokuProvider.GetGridClone();
            SelectGivenOnly();
            sudokuProvider.OnValueOrCandidatesChanged += () => _dirty = true;
        }

        private void Update()
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    var source = _sudokuProvider.Cells[x, y];
                    _grid.SetValue(x, y, _cellToValue(source));
                }
            }
            Converted = _converter.ToText(_grid);
            _dirty = false;
            OnChanged?.Invoke();
        }

        public void SelectGivenOnly()
        {
            SharedFields = SharedFields.Givens;
            _cellToValue = cell => cell.IsGiven ? cell.Input.Value : 0;
            Update();
        }

        public void SelectGivenAndUserInput()
        {
            SharedFields = SharedFields.GivensAndInputs;
            _cellToValue = cell => cell.Input.Value;
            Update();
        }
    }
}
