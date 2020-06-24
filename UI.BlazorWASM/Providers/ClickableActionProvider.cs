using Core.Data;
using Microsoft.AspNetCore.Components.Web;
using System;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Providers
{
    public class ClickableActionProvider : IClickableActionProvider
    {
        public IClickableAction ClickableAction { get; private set; }

        private InputValue _value;
        private Color _color1;
        private Color _color2;

        public InputValue Value
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
        public void SetClickableAction(IClickableAction clickableAction)
        {
            ClickableAction = clickableAction;
            OnChanged?.Invoke();
        }

        public ClickableActionProvider(ClickableActionFactory clickableActionFactory)
        {
            ClickableAction = clickableActionFactory.StandardAction();
            Value = InputValue.One;
            Color1 = Color.First;
            Color2 = Color.Second;
        }
        private ClickableActionArgs CreateArgs(MouseEventArgs e, int x, int y)
        {
            return new ClickableActionArgs
            {
                MouseEventArgs = e,
                Pos = new Position(x, y),
                Value = Value,
                Color1 = Color1,
                Color2 = Color2
            };
        }

        public void OnLeftClick(MouseEventArgs e, int x, int y)
        {
            ClickableAction.LeftClickAction(CreateArgs(e, x, y));
        }

        public void OnRightClick(MouseEventArgs e, int x, int y)
        {
            ClickableAction.RightClickAction(CreateArgs(e, x, y));
        }
    }
}
