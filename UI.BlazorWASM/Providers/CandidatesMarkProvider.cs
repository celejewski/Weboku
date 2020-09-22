using Core;
using Core.Data;
using System;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Providers
{
    public class CandidatesMarkProvider : IProvider
    {
        public CandidatesMarkProvider(DomainFacade domainFacade)
        {
            _domainFacade = domainFacade;
            domainFacade.OnCandidateChanged += () => OnChanged?.Invoke();
        }

        private readonly Color[,,] _colors = new Color[9, 9, 9];
        private readonly DomainFacade _domainFacade;

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

        public Color GetColor(Position pos, Value candidate)
        {
            var isLegal = !_domainFacade.HasCandidate(pos, candidate) || _domainFacade.IsCandidateLegal(pos, candidate);
            return isLegal ? _colors[pos.x, pos.y, candidate - 1] : Color.Illegal;
        }

        public event Action OnChanged;
    }
}
