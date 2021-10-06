using System;
using Weboku.Application.Enums;
using Weboku.Core.Data;

namespace Weboku.Application
{
    public sealed partial class DomainFacade
    {
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
    }
}