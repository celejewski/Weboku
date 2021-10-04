using Weboku.Core.Data;

namespace Weboku.Application.Filters
{
    public class EraseFilter : IFilter
    {
        public FilterOption IsFiltered(Grid grid, Position position)
        {
            return FilterOption.None;
        }
    }
}