using Core.Data;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Filters
{
    public class EraseFilter : IFilter
    {
        public FilterOption IsFiltered(ICell cell)
        {
            return FilterOption.None;
        }
    }
}
