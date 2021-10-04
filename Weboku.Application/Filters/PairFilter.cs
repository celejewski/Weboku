using Weboku.Core.Data;

namespace Weboku.Application.Filters
{
    public class PairFilter : IFilter
    {
        public FilterOption IsFiltered(Grid grid, Position position)
        {
            return grid.GetCandidatesCount(position) == 2
                ? FilterOption.Secondary
                : FilterOption.None;
        }
    }
}