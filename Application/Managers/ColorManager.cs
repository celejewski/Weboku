using System;
using Weboku.Application.Enums;
using Weboku.Core.Data;

namespace Weboku.Application.Managers
{
    public class ColorManager
    {
        private readonly Color[,] _cellColors = new Color[Position.Cols.Count, Position.Rows.Count];

        public event Action OnChanged;

        public Color GetColor(Position position) => _cellColors[position.x, position.y];

        public void SetColor(Position position, Color color)
        {
            _cellColors[position.x, position.y] = color;
            OnChanged?.Invoke();
        }

        public void ClearAll()
        {
            foreach (var position in Position.Positions)
            {
                _cellColors[position.x, position.y] = Color.None;
            }

            OnChanged?.Invoke();
        }
    }
}