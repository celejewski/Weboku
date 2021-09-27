using System;
using Microsoft.AspNetCore.Components.Web;
using Weboku.Core.Data;
using Weboku.UserInterface.ClickableActions;
using Weboku.UserInterface.Enums;

namespace Weboku.UserInterface.Providers
{
    public class ClickableActionProvider
    {
        private IClickableAction _clickableAction;

        private Value _value;
        private Color _color1;
        private Color _color2;
        private readonly ClickableActionFactory _clickableActionFactory;

        public Value Value
        {
            get => _value;
            set
            {
                _value = value;
                OnChanged?.Invoke();
            }
        }

        public Color Color1
        {
            get => _color1;
            set
            {
                _color1 = value;
                OnChanged?.Invoke();
            }
        }

        public Color Color2
        {
            get => _color2;
            set
            {
                _color2 = value;
                OnChanged?.Invoke();
            }
        }

        public event Action OnChanged;

        public void SelectClickableAction(ClickableAction clickableAction)
        {
            _clickableAction = _clickableActionFactory.MakeClickableAction(clickableAction);
            OnChanged?.Invoke();
        }

        public ClickableActionProvider(ClickableActionFactory clickableActionFactory)
        {
            _clickableAction = clickableActionFactory.MakeClickableAction(ClickableAction.Marker);
            Value = Value.One;
            Color1 = Color.First;
            Color2 = Color.Second;
            _clickableActionFactory = clickableActionFactory;
        }

        private ClickableActionArgs CreateArgs(MouseEventArgs e, Position position)
        {
            return new ClickableActionArgs
            {
                MouseEventArgs = e,
                Position = position,
                Value = Value,
                Color1 = Color1,
                Color2 = Color2
            };
        }

        public void OnLeftClick(MouseEventArgs e, Position position)
        {
            _clickableAction.LeftClickAction(CreateArgs(e, position));
        }

        public void OnRightClick(MouseEventArgs e, Position position)
        {
            _clickableAction.RightClickAction(CreateArgs(e, position));
        }
    }
}