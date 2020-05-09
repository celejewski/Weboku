using System.Collections.Generic;

namespace Core.Data
{
    public class Cell : ICell
    {
        public Cell()
        {
            Input = new CellInput
            {
                IsLegal = true,
                Value = 0,
            };

            Candidates = new Dictionary<int, ICellInput>();
        }

        public int X { get; set; }
        public int Y { get; set; }

        public int Block { get; set; }

        public bool IsGiven { get; set; }
        public CellInput Input { get; set; }

        ICellInput ICell.Input { get => Input; }

        public IDictionary<int, ICellInput> Candidates { get; }
    }
}
