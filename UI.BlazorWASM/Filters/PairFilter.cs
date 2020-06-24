using Core.Data;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Filters
{
    public class PairFilter : IFilter
    {
        public FilterOption IsFiltered(IGridProvider gridProvider, Position pos)
        {
            return gridProvider.GetCandidatesCount(pos) == 2 ? FilterOption.Secondary : FilterOption.None;
        }
    }
}
