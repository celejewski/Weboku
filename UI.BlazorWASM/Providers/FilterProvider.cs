using Application;
using Application.Filters;
using System;

namespace UI.BlazorWASM.Providers
{
    public class FilterProvider
    {
        private readonly DomainFacade _domainFacade;

        public IFilter Filter { get => _domainFacade.Filter; }

        public FilterProvider(DomainFacade domainFacade)
        {
            _domainFacade = domainFacade;
        }

        public event Action OnChanged;

        public void SetFilter(IFilter filter)
        {
            _domainFacade.Filter = filter;
            OnChanged?.Invoke();
        }
    }
}
