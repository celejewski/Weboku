using Weboku.Core.Data;

namespace Weboku.Core.Hints
{
    public class Hint
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Grid Grid { get; set; }
        public bool[] IsRowHighlighted { get; set; }
        public bool[] IsColHighlighted { get; set; }
        public bool[] IsBlockHighlighted { get; set; }
        public Color[,] CellColors { get; set; }
        public Color[,] InputColors { get; set; }
        public Color[,,] CandidatesColors { get; set; }
    }
}