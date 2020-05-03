using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Data
{
    public interface ICellInput
    {
        int Value { get; }
        bool IsLegal { get; }
    }
}
