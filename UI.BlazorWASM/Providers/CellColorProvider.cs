using Core.Data;
using System;
using UI.BlazorWASM.Converters;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Providers
{
    public class CellColorProvider
    {
        private readonly Color[,] _cellColors = new Color[Position.Cols.Count, Position.Rows.Count];

        public event Action OnChanged;

        public Color GetColor(Position position) => _cellColors[position.x, position.y];

        public string GetCssClass(Position position)
        {
            return CellColorConverter.ToCssClass(_cellColors[position.x, position.y]);
        }
        public void SetColor(Position position, Color color)
        {
            _cellColors[position.x, position.y] = color;
            OnChanged?.Invoke();
        }

        public void ClearAll()
        {
            foreach( var position in Position.Positions )
            {
                _cellColors[position.x, position.y] = Color.None;
            }
            OnChanged?.Invoke();
        }

    }
}