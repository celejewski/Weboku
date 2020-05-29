using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Filters
{
    public interface IFilter
    {
        /// <summary>
        /// Returns if cell should be highlighted or not.
        /// </summary>
        /// <param name="cell">Cell is only argument I need because Cell has X, Y props</param>
        /// <returns>CSS class "filter-false", "filter-true-primary", "filter-true-secondary"</returns>
        string IsFiltered(ICell cell);
    }
}
