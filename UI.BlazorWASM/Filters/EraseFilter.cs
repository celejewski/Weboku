using Core.Data;

namespace UI.BlazorWASM.Filters
{
    public class EraseFilter : IFilter
    {
        public string IsFiltered(ICell cell)
        {
            if( cell.Input.Value != 0 && !cell.IsGiven )
            {
                return FilterStyleClass.Primary;
            }
            else if (cell.Candidates.Count > 0)
            {
                return FilterStyleClass.Secondary;
            }

            return FilterStyleClass.None;
        }
    }
}
