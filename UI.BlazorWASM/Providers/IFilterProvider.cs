using UI.BlazorWASM.Filters;

namespace UI.BlazorWASM.Providers
{
    public interface IFilterProvider : IProvider
    {
        public IFilter Filter { get; }
        void SetFilter(IFilter filter);
    }
}
