﻿using Weboku.Core.Data;

namespace Weboku.Application.Filters
{
    public interface IFilter
    {
        /// <summary>
        /// Returns if cell should be highlighted or not.
        /// </summary>
        /// <param name="cell">Cell is only argument I need because Cell has X, Y props</param>
        /// <returns>CSS class "filter-false", "filter-true-primary", "filter-true-secondary"</returns>
        FilterOption IsFiltered(Grid grid, Position position);
    }
}