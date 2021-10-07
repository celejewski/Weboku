using System;

namespace Weboku.UserInterface.Providers
{
    public class TooltipProvider : IProvider
    {
        private bool _isVisible;
        private string _text;

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnHintsChanged?.Invoke();
            }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                OnHintsChanged?.Invoke();
            }
        }

        public event Action OnHintsChanged;
    }
}