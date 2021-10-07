using System;
using Weboku.Application.Enums;
using Weboku.Core.Data;

namespace Weboku.Application
{
    public sealed partial class DomainFacade
    {
        public Color GetCellColor(Position position)
        {
            return _cellColorManager.GetColor(position);
        }

        public void SetCellColor(Position position, Color color)
        {
            _cellColorManager.SetColor(position, color);
        }

        public void ClearAllCellColors()
        {
            _cellColorManager.ClearAll();
        }

        public event Action OnCellColorChanged;


        private readonly Color[,] _inputColors = new Color[9, 9];

        public void SetInputColor(Position position, Color color)
        {
            _inputColors[position.x, position.y] = color;
            OnInputColorChanged?.Invoke();
        }


        public Color GetInputColor(Position position)
        {
            return IsValueLegal(position)
                ? _inputColors[position.x, position.y]
                : Color.Illegal;
        }

        public void ClearInputColors()
        {
            foreach (var position in Position.Positions)
            {
                _inputColors[position.x, position.y] = Color.None;
            }

            OnInputColorChanged?.Invoke();
        }

        public event Action OnInputColorChanged;

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