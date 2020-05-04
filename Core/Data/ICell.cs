using System.Collections.Generic;

namespace Core.Data
{
    public interface ICell
    {
        int Row { get; }
        int Col { get; }
        int Block { get; }
        bool IsGiven { get; }
        ICellInput Input { get; }
        IDictionary<int, ICellInput> Candidates { get; }

    }
}
