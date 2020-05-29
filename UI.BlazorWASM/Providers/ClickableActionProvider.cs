using Microsoft.AspNetCore.Components.Web;
using System;
using UI.BlazorWASM.ClickableActions;

namespace UI.BlazorWASM.Providers
{
    public class ClickableActionProvider : IClickableActionProvider
    {
        public IClickableAction ClickableAction { get; private set; }

        private int _value;
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
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
            Value = 1;
        }
        private ClickableActionArgs CreateArgs(MouseEventArgs e, int x, int y)
        {
            return new ClickableActionArgs
            {
                MouseEventArgs = e,
                X = x,
                Y = y,
                Value = Value,
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
