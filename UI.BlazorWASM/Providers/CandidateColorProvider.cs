using System;
using Weboku.Application;
using Weboku.Core.Data;
using Weboku.UserInterface.Enums;

namespace Weboku.UserInterface.Providers
{
    public class CandidateColorProvider : IProvider
    {
        public CandidateColorProvider(DomainFacade domainFacade)
        {
            _colors = new Color[
                Position.Cols.Count,
                Position.Rows.Count,
                Value.All.Count];
            domainFacade.OnGridChanged += () => OnChanged?.Invoke();
            _domainFacade = domainFacade;
        }

        private readonly Color[,,] _colors;
        private readonly DomainFacade _domainFacade;

        public void SetColor(Position position, Value value, Color color)
        {
            _colors[position.x, position.y, value] = color;
            OnChanged?.Invoke();
        }

        public void ClearColors()
        {
            foreach (var position in Position.Positions)
            {
                foreach (var value in Value.All)
                {
                    _colors[position.x, position.y, value] = Color.None;
                }
            }

            OnChanged?.Invoke();
        }

        public Color GetColor(Position position, Value value)
        {
            var isCandidateLegal = !_domainFacade.HasCandidate(position, value)
                                   || _domainFacade.IsCandidateLegal(position, value);

            return isCandidateLegal
                ? _colors[position.x, position.y, value]
                : Color.Illegal;
        }

        public event Action OnChanged;
    }
}