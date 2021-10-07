using System;

namespace Weboku.Application
{
    public sealed partial class DomainFacade
    {
        private bool _isTooltipVisible;
        private string _tooltipText;

        public string TooltipText
        {
            get => _tooltipText;
            set
            {
                _tooltipText = value;
                OnTooltipChanged?.Invoke();
            }
        }

        public bool IsTooltipVisible
        {
            get => _isTooltipVisible;
            set
            {
                _isTooltipVisible = value;
                OnTooltipChanged?.Invoke();
            }
        }

        public event Action OnTooltipChanged;
    }
}