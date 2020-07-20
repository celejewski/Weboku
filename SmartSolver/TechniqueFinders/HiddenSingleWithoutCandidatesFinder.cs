﻿using Core.Data;
using SmartSolver.SolvingTechniques;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolver.TechniqueFinders
{
    public class HiddenSingleWithoutCandidatesFinder : BaseTechniqueFinder
    {
        private readonly ISolvingTechniquesFactory _factory;

        public HiddenSingleWithoutCandidatesFinder(ISolvingTechniquesFactory factory)
        {
            _factory = factory;
        }

        public override IEnumerable<ISolvingTechnique> FindAll(IGrid grid)
        {
            foreach( var house in Position.Houses )
            {
                foreach( var value in InputValue.NonEmpty )
                {
                    var positionsWhereValueCanGo = house
                        .Where(pos => !grid.HasValue(pos))
                        .Where(pos1 => GridHelper.GetCoordsWhichCanSee(pos1)
                            .All(pos2 => grid.GetValue(pos2) != value)); ;

                    if (positionsWhereValueCanGo.Count() == 1)
                    {
                        var pos = positionsWhereValueCanGo.First();
                        yield return _factory.HiddenSingle(pos, value);
                    }
                }
            }
        }
    }
}
