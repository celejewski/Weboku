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
                OnLanguageChanged?.Invoke();
            }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                OnLanguageChanged?.Invoke();
            }
        }

        public event Action OnLanguageChanged;
    }
}