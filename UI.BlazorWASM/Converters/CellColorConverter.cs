using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Converters
{
    public class CellColorConverter
    {
        public static string ToCssClass(Color cellColor)
        {

            return cellColor switch
            {
                Color.Legal => "cell-color-legal",
                Color.Illegal => "cell-color-illegal",
                Color.Info => "cell-color-info",
                Color.First => "cell-color-first",
                Color.Second => "cell-color-second",
                Color.Third => "cell-color-third",
                Color.Fourth => "cell-color-fourth",
                Color.None => string.Empty,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
