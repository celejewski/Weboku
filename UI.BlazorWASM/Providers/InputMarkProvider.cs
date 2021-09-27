using System;
using Application;
using Core.Data;
using Weboku.UserInterface.Enums;

namespace Weboku.UserInterface.Providers
{
    public class InputMarkProvider : IProvider
    {
        private readonly Color[,] _colors = new Color[9, 9];
        private readonly DomainFacade _domainFacade;

        public InputMarkProvider(DomainFacade domainFacade)
        {
            _domainFacade = domainFacade;
            _domainFacade.OnGridChanged += () => OnChanged?.Invoke();
        }

        public void SetColor(Position position, Color color)
        {
            _colors[position.x, position.y] = color;
            OnChanged?.Invoke();
        }


        public Color GetColor(Position position)
        {
            return _domainFacade.IsValueLegal(position)
                ? _colors[position.x, position.y]
                : Color.Illegal;
        }

        public void ClearColors()
        {
            foreach (var position in Position.Positions)
            {
                _colors[position.x, position.y] = Color.None;
            }

            OnChanged?.Invoke();
        }

        public event Action OnChanged;
    }
}