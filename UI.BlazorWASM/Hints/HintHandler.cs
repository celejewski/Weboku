using System.Collections.Generic;

namespace UI.BlazorWASM.Hints
{
    public abstract class HintHandler
    {
        protected HintHandler _next;
        public abstract void Execute(string step, IEnumerator<string> enumerator);
        public HintHandler SetNext(HintHandler next)
        {
            _next = next;
            return next;
        }
    }
}
