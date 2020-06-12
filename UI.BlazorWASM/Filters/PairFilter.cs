using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Filters
{
    public class PairFilter : IFilter
    {
        public FilterOption IsFiltered(IGridProvider gridProvider, int x, int y)
        {
            return gridProvider.GetCandidatesCount(x, y) == 2 ? FilterOption.Secondary : FilterOption.None;
        }
    }
}
