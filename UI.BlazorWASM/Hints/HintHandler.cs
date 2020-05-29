using System.Collections.Generic;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Hints
{
    public abstract class HintHandler
    {
        protected HintHandler _next;
        public abstract Task Execute(string step, IEnumerator<string> enumerator);
        public HintHandler SetNext(HintHandler next)
        {
            _next = next;
            return next;
        }
    }
}
