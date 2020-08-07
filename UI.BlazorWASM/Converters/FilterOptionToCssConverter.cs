using System;
using UI.BlazorWASM.Filters;

namespace UI.BlazorWASM.Converters
{
    public class FilterOptionToCssConverter
    {
        public static string ToCssClass(FilterOption filterOption)
        {
            return filterOption switch
            {
                FilterOption.None => "filter__cell--none",
                FilterOption.Primary => "filter__cell--primary",
                FilterOption.Secondary => "filter__cell--secondary",
                _ => throw new NotImplementedException(),
            };
        }
    }
}
