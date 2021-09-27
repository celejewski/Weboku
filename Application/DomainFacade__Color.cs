using System;
using Weboku.Application.Enums;
using Weboku.Core.Data;

namespace Weboku.Application
{
    public sealed partial class DomainFacade
    {
        public Color GetColor(Position position)
        {
            return _colorManager.GetColor(position);
        }

        public void SetColor(Position position, Color color)
        {
            _colorManager.SetColor(position, color);
        }

        public void ClearAllColors()
        {
            _colorManager.ClearAll();
        }

        public event EventHandler OnColorChanged;
    }
}