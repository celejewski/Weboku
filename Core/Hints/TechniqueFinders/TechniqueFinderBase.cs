﻿using Core.Data;
using Core.Hints.SolvingTechniques;
using System.Collections.Generic;
using System.Linq;

namespace Core.Hints.TechniqueFinders
{
    public abstract class TechniqueFinderBase : ITechniqueFinder
    {
        public ISolvingTechnique Find(Grid grid)
        {
            return FindAll(grid).First();
        }

        public abstract IEnumerable<ISolvingTechnique> FindAll(Grid grid);
    }
}
