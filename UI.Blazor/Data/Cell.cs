using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Blazor.Data
{
    public class Cell
    {
        public int Value { get; set; }
        public bool IsValid { get; set; }
        public bool IsGiven { get; set; }

        public bool HasValue { get => Value != 0; }

        public IEnumerable<Candidate> Candidates { get; set; } = new List<Candidate>();

        public bool HasTopBorder { get; set; }

        public bool HasBottomBorder { get; set; }

        public bool HasLeftBorder { get; set; }

        public bool HasRightBorder { get; set; }
    }
}
