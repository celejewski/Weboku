using System.Collections.Generic;

namespace Core.Data
{
    public interface ICell
    {
        int X { get; }
        int Y { get; }
        int Block { get; }
        bool IsGiven { get; }
        ICellInput Input { get; }
        IDictionary<int, ICellInput> Candidates { get; }

    }
}
