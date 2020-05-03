using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Data
{
    public class CellInput : ICellInput
    {
        public int Value { get; set; }

        public bool IsLegal { get; set; }
    }
}
