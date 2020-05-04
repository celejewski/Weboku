using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Data
{
    public class CellInput : ICellInput, ICloneable
    {
        public int Value { get; set; }

        public bool IsLegal { get; set; }

        public object Clone()
        {
            return new CellInput { Value = Value, IsLegal = IsLegal };
        }
    }
}
