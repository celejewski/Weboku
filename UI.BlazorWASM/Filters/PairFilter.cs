using Core.Data;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Filters
{
    public class PairFilter : IFilter
    {
        public FilterOption IsFiltered(ICell cell)
        {
            if (cell.Candidates.Count == 2)
            {
                return FilterOption.Secondary;
            }
            else
            {
                return FilterOption.None;
            }
        }
    }
}
