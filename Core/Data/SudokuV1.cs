using System.Collections.Generic;

namespace Core.Data
{
    public class SudokuV1
    {
        public string Given { get; set; }
        public string Solution { get; set; }
        public int Rating { get; set; }
        public IEnumerable<string> Steps { get; set; } = new List<string>();
        public string Difficulty { get; set; }
    }
}
