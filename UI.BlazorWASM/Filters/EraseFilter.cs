using Core.Data;

namespace UI.BlazorWASM.Filters
{
    public class EraseFilter : IFilter
    {
        public string IsFiltered(ICell cell)
        {
            return FilterStyleClass.None;
        }
    }
}
