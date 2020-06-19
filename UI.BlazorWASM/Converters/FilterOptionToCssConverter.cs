using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.BlazorWASM.Enums;

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
