using Core.Hints.SolvingTechniques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public class NoSolutionDisplayer : BaseSolvingTechniqueDisplayer
    {
        public NoSolutionDisplayer(Informer informer, Displayer displayer, ISolvingTechnique noSolution) 
            : base(informer, displayer, noSolution, "no-solution")
        {
        }
    }
}
