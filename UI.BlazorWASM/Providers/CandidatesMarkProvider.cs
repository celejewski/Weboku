using Core.Data;
using System;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Providers
{
    public class CandidatesMarkProvider : IProvider
    {
        public CandidatesMarkProvider(IGridProvider gridProvider)
        {
            _gridProvider = gridProvider;
            _gridProvider.OnValueOrCandidatesChanged += () => OnChanged?.Invoke();
        }

        private readonly Color[,,] _colors = new Color[9, 9, 9];
        private readonly IGridProvider _gridProvider;

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

        public Color GetColor(Position pos, InputValue candidate)
        {
            var isLegal = !_gridProvider.HasCandidate(pos, candidate) || _gridProvider.IsCandidateLegal(pos, candidate);
            return isLegal ? _colors[pos.x, pos.y, candidate - 1] : Color.Illegal;
        }

        public event Action OnChanged;
    }
}
