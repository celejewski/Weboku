using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Converters
{
    public class CellColorConverter
    {
        public static string ToCssClass(CellColor cellColor)
        {

            return cellColor switch
            {
                CellColor.Legal => "cell-color-legal",
                CellColor.Illegal => "cell-color-illegal",
                CellColor.First => "cell-color-first",
                CellColor.Second => "cell-color-second",
                CellColor.Third => "cell-color-third",
                CellColor.Fourth => "cell-color-fourth",
                CellColor.None => "",
                _ => throw new NotImplementedException(),
            };
        }
    }
}
