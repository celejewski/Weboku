using System;
using Weboku.Application.Enums;
using Weboku.Core.Data;

namespace Weboku.Application
{
    public sealed partial class DomainFacade
    {
        private readonly Color[,,] _candidateColors;

        public void SetColor(Position position, Value value, Color color)
        {
            _candidateColors[position.x, position.y, value] = color;
            OnCandidateColorChanged?.Invoke();
        }

        public void ClearCandidatesColors()
        {
            foreach (var position in Position.Positions)
            {
                foreach (var value in Value.All)
                {
                    _candidateColors[position.x, position.y, value] = Color.None;
                }
            }

            OnCandidateColorChanged?.Invoke();
        }

        public Color GetCandidateColor(Position position, Value value)
        {
            var isCandidateLegal = !HasCandidate(position, value) || IsCandidateLegal(position, value);

            return isCandidateLegal
                ? _candidateColors[position.x, position.y, value]
                : Color.Illegal;
        }

        public event Action OnCandidateColorChanged;
    }
}