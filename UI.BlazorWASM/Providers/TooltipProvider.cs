using System;

namespace UI.BlazorWASM.Providers
{
    public class TooltipProvider : IProvider
    {
        private bool _isVisible;
        private string _text;

        public string Text
        {
            get => _text; set
            {

                _text = value;
                OnChanged?.Invoke();
            }
        }
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                OnChanged?.Invoke();
            }
        }

        public event Action OnChanged;
    }
}
