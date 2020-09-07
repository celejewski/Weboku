using Core.Data;
using Core.Hints.SolvingTechniques;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Hints.TechniqueFinders
{
    public class HiddenSingleWithoutCandidatesFinder : HiddenSingleFinder
    {
        public override IEnumerable<ISolvingTechnique> FindAll(IGrid input)
        {
            var grid = input.Clone();
            grid.FillAllLegalCandidates();
            return base.FindAll(grid);
        }
    }
}
