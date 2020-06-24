using Core.Data;
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

        public FilterOption IsFiltered(IGridProvider gridProvider, Position pos)
        {
            return _shareProvider.SharedFields switch
            {
                SharedFields.Givens => gridProvider.GetIsGiven(pos) ? FilterOption.Primary : FilterOption.None,
                SharedFields.GivensAndInputs => gridProvider.HasValue(pos) ? FilterOption.Primary : FilterOption.None,
                SharedFields.Everything => FilterOption.Primary,
                _ => FilterOption.None,
            };
        }
    }
}
