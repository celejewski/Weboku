using Weboku.Application.Enums;
using Weboku.Core.Data;

namespace Weboku.Application.Filters
{
    public class SharedFilter : IFilter
    {
        private readonly SharedFields _sharedFields;

        public SharedFilter(SharedFields sharedFields)
        {
            _sharedFields = sharedFields;
        }

        public FilterOption IsFiltered(Grid grid, Position position)
        {
            return _sharedFields switch
            {
                SharedFields.Givens => grid.GetIsGiven(position) ? FilterOption.Primary : FilterOption.None,
                SharedFields.GivensAndInputs => grid.HasValue(position) ? FilterOption.Primary : FilterOption.None,
                SharedFields.Everything => FilterOption.Primary,
                _ => FilterOption.None,
            };
        }
    }
}