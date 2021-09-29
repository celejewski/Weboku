using Weboku.Core.Data;

namespace Weboku.Application.Filters
{
    public class PairFilter : IFilter
    {
        public FilterOption IsFiltered(DomainFacade domainFacade, Position pos)
        {
            return domainFacade.GetCandidatesCount(pos) == 2 ? FilterOption.Secondary : FilterOption.None;
        }
    }
}