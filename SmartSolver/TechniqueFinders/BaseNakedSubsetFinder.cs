﻿using Core.Data;
using Core.Helpers;
using SmartSolver.SolvingTechniques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSolver.TechniqueFinders
{
    public abstract class BaseNakedSubsetFinder : BaseTechniqueFinder
    {

        protected IEnumerable<(IEnumerable<Position> positions, IEnumerable<InputValue> values)> NakedSubset(IGrid grid, int depth)
        {
            foreach( var house in Position.Houses )
            {
                var positionsInHouse = house
                    .WithoutValue(grid)
                    .Where(pos => grid.CandidatesCount(pos) <= depth);

                if( positionsInHouse.Count() < depth ) continue;

                var result = NakedSubsetStep(grid, new List<Position>(), new HashSet<InputValue>(), positionsInHouse, depth, 0);
                foreach (var item in result)
                {
                    yield return item;
                }
            }
        }

        private IEnumerable<(IEnumerable<Position> positions, IEnumerable<InputValue> values)> NakedSubsetStep(
            IGrid grid,
            List<Position> positions,
            HashSet<InputValue> values,
            IEnumerable<Position> house,
            int depth,
            int index)
        {
            if( values.Count > depth ) yield break;
            if( positions.Count == depth )
            {
                var positionsSeen = Position.GetOtherPositionsSeenBy(positions);
                if( values.Any(value => positionsSeen.WithCandidate(grid, value).Any()) ) yield return (positions, values);
                else yield break;
            }

            foreach( var pos in house.Skip(index + 1) )
            {
                index += 1;
                var positionsNew = new List<Position>(positions) { pos };
                var valuesNew = new HashSet<InputValue>(values);
                valuesNew.UnionWith(grid.GetCandidates(pos));

                var result = NakedSubsetStep(grid, positionsNew, valuesNew, house, depth, index);
                foreach( var item in result )
                {
                    yield return item;
                }
            }
        }
    }
}
