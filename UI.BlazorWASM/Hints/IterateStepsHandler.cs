using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Hints
{
    public class IterateStepsHandler : HintHandler
    {
        public override Task Execute(string step, IEnumerator<string> enumerator)
        {
            Console.WriteLine(step + " IterateStepsHandler ");
            if (enumerator.MoveNext())
            {
                return _next?.Execute(enumerator.Current, enumerator);
            }
            return Task.CompletedTask;
        }
    }
}
