using Application;
using Core.Data;

namespace UI.BlazorWASM.Filters
{
    public class PairFilter : IFilter
    {
        public FilterOption IsFiltered(DomainFacade gridProvider, Position pos)
        {
            return gridProvider.GetCandidatesCount(pos) == 2 ? FilterOption.Secondary : FilterOption.None;
        }
    }
}
