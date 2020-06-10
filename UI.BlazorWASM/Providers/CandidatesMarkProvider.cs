using System;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Providers
{
    public class CandidatesMarkProvider : IProvider
    {
        private readonly Color[,,] _colors = new Color[9, 9, 9];
        public void SetColor(int x, int y, int candidate, Color value)
        {
            _colors[x, y, candidate - 1] = value;
            OnChanged?.Invoke();
        }

        public void ClearColors()
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    for( int value = 0; value < 9; value++ )
                    {
                        _colors[x, y, value] = Color.None;
                    }
                }
            }
            OnChanged?.Invoke();
        }

        public Color GetColor(int x, int y, int candidate) => _colors[x, y, candidate - 1];

        public event Action OnChanged;
    }
}
