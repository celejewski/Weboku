using Core.Data;
using UI.BlazorWASM.Enums;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Filters
{
    public interface IFilter
    {
        /// <summary>
        /// Returns if cell should be highlighted or not.
        /// </summary>
        /// <param name="cell">Cell is only argument I need because Cell has X, Y props</param>
        /// <returns>CSS class "filter-false", "filter-true-primary", "filter-true-secondary"</returns>
        FilterOption IsFiltered(IGridProvider gridProvider, Position pos);
    }
}
