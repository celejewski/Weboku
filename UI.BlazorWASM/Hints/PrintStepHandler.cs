using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Hints
{
    public class PrintStepHandler : HintHandler
    {
        public override Task Execute(string step, IEnumerator<string> enumerator)
        {
            Console.WriteLine(step + " PrintStep");
            return _next?.Execute(step, enumerator);
        }
    }
}
