using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Filters
{
    public class EraseFilter : IFilter
    {
        public FilterOption IsFiltered(IGridProvider gridProvider, int x, int y)
        {
            return FilterOption.None;
        }
    }
}
