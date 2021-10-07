using System;
using Weboku.Application.Enums;
using Weboku.Core.Data;

namespace Weboku.Application.Managers
{
    public class ColorManager
    {
        private readonly Color[,] _cellColors = new Color[Position.Cols.Count, Position.Rows.Count];

        public event Action OnCellColorChanged;

        public Color GetCellColor(Position position) => _cellColors[position.x, position.y];

        public void SetCellColor(Position position, Color color)
        {
            _cellColors[position.x, position.y] = color;
            OnCellColorChanged?.Invoke();
        }

        public void Clear()
        {
            ClearInputColors();
            ClearCandidatesColors();
            ClearAllCellColors();
        }

        public void ClearAllCellColors()
        {
            foreach (var position in Position.Positions)
            {
                _cellColors[position.x, position.y] = Color.None;
            }

            OnCellColorChanged?.Invoke();
        }


        private readonly Color[,] _inputColors = new Color[9, 9];

        public void SetInputColor(Position position, Color color)
        {
            _inputColors[position.x, position.y] = color;
            OnInputColorChanged?.Invoke();
        }


        public Color GetInputColor(Position position)
        {
            return _inputColors[position.x, position.y];
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

        private readonly Color[,,] _candidateColors = new Color[9, 9, 10];

        public void SetCandidateColor(Position position, Value value, Color color)
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
            return _candidateColors[position.x, position.y, value];
        }

        public event Action OnCandidateColorChanged;
    }
}