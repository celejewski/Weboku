using System;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Helpers;

namespace UI.BlazorWASM.Providers
{
    public class MarkInputProvider : IProvider
    {
        private readonly Color[,] _colors = new Color[9, 9];

        public void SetColor(Position position, Color color)
        {
            SetColor(position.X, position.Y, color);
        }

        public void SetColor(int x, int y, Color color)
        {
            _colors[x, y] = color;
            OnChanged?.Invoke();
        }

        public Color GetColor(int x, int y) => _colors[x, y];

        public void ClearColors()
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    _colors[x, y] = Color.None;
                }
            }
            OnChanged?.Invoke();
        }

        public event Action OnChanged;
    }
}
