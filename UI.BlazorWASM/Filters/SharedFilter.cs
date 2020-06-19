using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Filters
{
    public class SharedFilter : IFilter
    {
        private readonly ShareProvider _shareProvider;

        public SharedFilter(ShareProvider shareProvider)
        {
            _shareProvider = shareProvider;
        }

        public FilterOption IsFiltered(IGridProvider gridProvider, int x, int y)
        {
            return _shareProvider.SharedFields switch
            {
                SharedFields.Givens => gridProvider.GetIsGiven(x, y) ? FilterOption.Primary : FilterOption.None,
                SharedFields.GivensAndInputs => gridProvider.HasValue(x, y) ? FilterOption.Primary : FilterOption.None,
                SharedFields.Everything => FilterOption.Primary,
                _ => FilterOption.None,
            };
        }
    }
}
