using Weboku.Core.Data;

namespace Weboku.Application.Filters
{
    public class EraseFilter : IFilter
    {
        public FilterOption IsFiltered(DomainFacade domainFacade, Position pos)
        {
            return FilterOption.None;
        }
    }
}