using System;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Converters
{
    public static class CellColorConverter
    {
        public static string ToCssClass(Color cellColor)
        {
            return cellColor switch
            {
                Color.Legal => "color-legal",
                Color.Illegal => "color-illegal",
                Color.Info => "color-info",
                Color.First => "color-first",
                Color.Second => "color-second",
                Color.Third => "color-third",
                Color.Fourth => "color-fourth",
                Color.None => string.Empty,
            };
        }
    }
}
