using System.Collections.Generic;

namespace Weboku.Core.Data
{
    public class Sudoku
    {
        public string Given { get; set; }
        public IEnumerable<string> Steps { get; set; } = new List<string>();
        public string Difficulty { get; set; }
    }
}