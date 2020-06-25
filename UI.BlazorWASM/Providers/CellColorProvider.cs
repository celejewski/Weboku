using Core.Data;
using System;
using UI.BlazorWASM.Converters;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Providers
{
    public class CellColorProvider
    {
        private readonly Color[,] _cellColors = new Color[9, 9];

        public event Action OnChanged;

        public Color GetColor(int x, int y) => _cellColors[x, y];

        public string GetCssClass(Position pos)
        {
            return CellColorConverter.ToCssClass(_cellColors[pos.x, pos.y]);
        }
        public void SetColor(int x, int y, Color color)
        {
            _cellColors[x, y] = color;
            OnChanged?.Invoke();
        }

        public void ToggleColor(Position pos, Color color)
        {
            _cellColors[pos.x, pos.y] = _cellColors[pos.x, pos.y] == color ? Color.None : color;
            OnChanged?.Invoke();
        }

        public void ClearAll()
        {
            for( int y = 0; y < 9; y++ )
            {
                for( int x = 0; x < 9; x++ )
                {
                    _cellColors[x, y] = Color.None;
                }
            }
            OnChanged?.Invoke();
        }

    }
}