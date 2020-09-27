using Application;
using Core.Data;

namespace UI.BlazorWASM.Filters
{
    public class EraseFilter : IFilter
    {
        public FilterOption IsFiltered(DomainFacade gridProvider, Position pos)
        {
            return FilterOption.None;
        }
    }
}
