using Core.Data;

namespace UI.BlazorWASM.Filters
{
    public class PairFilter : IFilter
    {
        public string IsFiltered(ICell cell)
        {
            if (cell.Candidates.Count == 2)
            {
                return FilterStyleClass.Secondary;
            }
            else
            {
                return FilterStyleClass.None;
            }
        }
    }
}
