using SmartSolver.SolvingTechniques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public class InvalidValuesDisplayer : BaseSolvingTechniqueDisplayer
    {
        public InvalidValuesDisplayer(InvalidValue invalidValue) 
            :base(invalidValue, "invalid-solution")
        {

        }
    }
}
