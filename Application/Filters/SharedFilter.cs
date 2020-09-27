using Application.Data;
using Core.Data;

namespace Application.Filters
{
    public class SharedFilter : IFilter
    {
        public FilterOption IsFiltered(DomainFacade domainProvider, Position pos)
        {
            return domainProvider.SharedFields switch
            {
                SharedFields.Givens => domainProvider.IsGiven(pos) ? FilterOption.Primary : FilterOption.None,
                SharedFields.GivensAndInputs => domainProvider.HasValue(pos) ? FilterOption.Primary : FilterOption.None,
                SharedFields.Everything => FilterOption.Primary,
                _ => FilterOption.None,
            };
        }
    }
}
