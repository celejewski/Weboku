using System;
using UI.BlazorWASM.Converters;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Providers
{
    public class CellColorProvider : ICellColorProvider
    {
        private readonly CellColor[,] _cellColors = new CellColor[9, 9];
        
        public event Action OnChanged;
        
        public CellColor GetColor(int x, int y) => _cellColors[x, y];
        
        public string GetCssClass(int x, int y)
        {
            return CellColorConverter.ToCssClass(_cellColors[x, y]);
        }
        public void SetColor(int x, int y, CellColor color)
        {
            _cellColors[x, y] = color;
            OnChanged?.Invoke();
        }

        public void ToggleColor(int x, int y, CellColor color)
        {
            _cellColors[x, y] = _cellColors[x, y] == color ? CellColor.None : color;
            OnChanged?.Invoke();
        }

        public void ClearAll()
        {
            for( int y = 0; y < 9; y++ )
            {
                for( int x = 0; x < 9; x++ )
                {
                    _cellColors[x, y] = CellColor.None;
                }
            }
            OnChanged?.Invoke();
        }

    }
}