using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


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

            Candidates = new List<ICellInput>();
        }
        public int Row { get; set; }

        public int Col { get; set; }

        public int Block { get; set; }

        public bool IsGiven { get; set; }
        public CellInput Input { get; set; }

        ICellInput ICell.Input { get => Input; }

        public IList<ICellInput> Candidates { get; }
    }
}
