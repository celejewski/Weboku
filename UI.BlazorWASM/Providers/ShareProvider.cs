using Core.Converters;
using Core.Data;
using System;
using System.Threading.Tasks;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Providers 
{
    public class ShareProvider : IProvider
    {
        public string Converted { get => _converter.ToText(Grid); }
        private readonly IGridConverter _converter;
        public event Action OnChanged;
        private readonly ISudokuProvider _sudokuProvider;
        public readonly IGrid Grid;
        public SharedFields SharedFields { get; set; } = SharedFields.GivensAndInputs;

        public ShareProvider(ISudokuProvider sudokuProvider, HodokuGridConverter hodokuGridConverter)
        {
            _sudokuProvider = sudokuProvider;
            _converter = hodokuGridConverter;
            Grid = sudokuProvider.GetGridClone();
        }

        private void Update()
        {
            OnChanged?.Invoke();
        }

        public Task SelectGivenOnly()
        {
            SharedFields = SharedFields.Givens;
            Convert(cell => cell.IsGiven ? cell.Input.Value : 0);
            Update();
            return Task.CompletedTask;
        }

        public Task SelectGivenAndUserInput()
        {
            SharedFields = SharedFields.GivensAndInputs;
            Convert(cell => cell.Input.Value);
            Update();
            return Task.CompletedTask;
        }

        private void Convert(Func<ICell, int> CellToValue)
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    var source = _sudokuProvider.Cells[x, y];
                    Grid.SetValue(x, y, CellToValue(source));
                }
            }
        }
    }
}
