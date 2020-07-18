﻿using SmartSolver.SolvingTechniques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Hints.SolvingTechniqueDisplayers
{
    public class NoSolutionDisplayer : BaseSolvingTechniqueDisplayer
    {
        public NoSolutionDisplayer(ISolvingTechnique noSolution) 
            : base(noSolution, "no-solution")
        {
        }
    }
}