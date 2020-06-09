using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Providers
{
    public class HintsProvider
    {
        public bool ShowHints { get; set; }

        public bool[] IsBlockHighlighted { get; private set; } = new bool[9];
        public bool[] IsColHighlighted { get; private set; } = new bool[9];
        public bool[] IsRowHighlighted { get; private set; } = new bool[9];

        public HintsProvider()
        {
        }

    }
}
