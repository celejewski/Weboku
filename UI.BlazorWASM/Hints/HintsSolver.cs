using Core.Data;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UI.BlazorWASM.Hints.SolvingTechniques;

namespace UI.BlazorWASM.Hints
{
    public class HintsSolver
    {
        private static readonly Func<IGrid, ISolvingTechnique>[] _steps
            = new Func<IGrid, ISolvingTechnique>[]
        {
            FullHouse,
            NakedSingle,
            HiddenSingle,
            LockedCandidatesPointing,
            LockedCandidatesClaiming,
            NakedPair,
            NakedTriple,
            HiddenPair,
            NakedQuadruple,
            HiddenTriple
        };
        public static ISolvingTechnique NextStep(IGrid input)
        {
            return _steps.Select(step => step(input))
                .FirstOrDefault(solvingTechnique => solvingTechnique != null)
                ?? new NotFound();
        }

        #region Singles
        private static ISolvingTechnique FullHouse(IGrid grid)
        {
            foreach( var indexes in Position.Houses )
            {
                if( indexes.WithValue(grid).Count() == 8 )
                {
                    var value = InputValue.NonEmpty.First(
                        value => indexes.All(index => grid.GetValue(index) != value));
                    var pos = indexes.First(index => !grid.HasValue(index));

                    return new FullHouse(pos, value);
                }
            }
            return null;
        }
        private static ISolvingTechnique NakedSingle(IGrid input)
        {
            foreach( var pos in Position.All )
            {
                if( !input.HasValue(pos)
                    && input.CandidatesCount(pos) == 1 )
                {
                    var value = InputValue.NonEmpty.First(value => input.HasCandidate(pos, value));
                    return new NakedSingle(pos, value);
                }
            }
            return null;
        }
        private static ISolvingTechnique HiddenSingle(IGrid grid)
        {
            foreach( var positions in Position.Houses )
            {
                foreach( var value in InputValue.NonEmpty )
                {
                    var positionsWithCandidate = positions.WithCandidate(grid, value);
                    if( positionsWithCandidate.Count() == 1 )
                    {
                        var pos = positionsWithCandidate.First();
                        return new HiddenSingle(pos, value);
                    }
                }
            }
            return null;
        }
        #endregion

        #region Locked Candidates
        private static ISolvingTechnique LockedCandidatesPointing(IGrid grid)
        {
            foreach( var value in InputValue.NonEmpty )
            {
                foreach( var block in Position.Blocks )
                {
                    var positionsInBlock = block.WithCandidate(grid, value);
                    var positionsToRemove = Position.GetOtherPositionsSeenBy(positionsInBlock)
                        .WithCandidate(grid, value);

                    if( positionsToRemove.Any() )
                    {
                        var first = positionsInBlock.First();
                        return new LockedCandidatesPointing(first.block, value, positionsToRemove);
                    }
                }
            }
            return null;
        }

        private static ISolvingTechnique LockedCandidatesClaiming(IGrid grid)
        {
            foreach( var value in InputValue.NonEmpty )
            {
                foreach( var house in Position.Rows.Concat(Position.Cols) )
                {
                    var positionsInHouse = house.WithCandidate(grid, value);
                    var positionsToRemove = Position.GetOtherPositionsSeenBy(positionsInHouse)
                        .WithCandidate(grid, value);

                    if( positionsToRemove.Any() )
                    {
                        return new LockedCandidatesClaiming(value, positionsToRemove, Position.GetHouse(positionsInHouse));
                    }
                }
            }
            return null;
        }
        #endregion

        #region Naked Subset
        private static ISolvingTechnique NakedPair(IGrid input)
        {
            var result = NakedSubset(input, 2);
            return result != default
                ? new NakedPair(result.positions, result.values) 
                : null;
        }

        private static ISolvingTechnique NakedTriple(IGrid input)
        {
            var result = NakedSubset(input, 3);
            return result != default
                ? new NakedSubset(result.positions, result.values)
                : null;
        }

        private static ISolvingTechnique NakedQuadruple(IGrid input)
        {
            var result = NakedSubset(input, 4);
            return result != default
                ? new NakedSubset(result.positions, result.values)
                : null;
        }

        private static (IEnumerable<Position> positions, IEnumerable<InputValue> values) NakedSubset(IGrid input, int depth)
        {
            foreach( var house in Position.Houses )
            {
                var result = NakedSubsetStep(input, new List<Position>(), new HashSet<InputValue>(), house, depth);
                if( result != default )
                {
                    return result;
                }
            }
            return default;
        }

        private static (IEnumerable<Position> positions, IEnumerable<InputValue> values) NakedSubsetStep(
            IGrid input,
            List<Position> positions,
            HashSet<InputValue> values,
            IEnumerable<Position> house,
            int depth)
        {
            if( values.Count > depth )
            {
                return default;
            }

            if( positions.Count == depth )
            {
                var first = positions.First();
                var positionsSeenBy = HintsHelper.GetHouses(positions)
                    .SelectMany(house => HintsHelper.GetPositionsInHouse(first, house))
                    .Except(positions);

                if( values.Any(value => positionsSeenBy.Any(pos => input.HasCandidate(pos, value))))
                {
                    return (positions, values);
                }
            }

            var positionsInHouse = house
                .Where(pos => !input.HasValue(pos) && input.CandidatesCount(pos) <= depth)
                .Except(positions);

            foreach( var pos in positionsInHouse )
            {
                var positionsNew = new List<Position>(positions)
                {
                    pos
                };
                var valuesNew = new HashSet<InputValue>(values);
                foreach( var value in InputValue.NonEmpty )
                {
                    if( input.HasCandidate(pos, value) )
                    {
                        valuesNew.Add(value);
                    }
                }

                var result = NakedSubsetStep(input, positionsNew, valuesNew, house, depth);
                if( result != default )
                {
                    return result;
                }
            }
            return default;
        }
        #endregion

        #region Hidden Subset
        public static ISolvingTechnique HiddenPair(IGrid input)
        {
            var result = HiddenSubset(input, 2);
            return result != default
                ? new HiddenPair(result.positions, result.values)
                : default;
        }

        public static ISolvingTechnique HiddenTriple(IGrid input)
        {
            var result = HiddenSubset(input, 3);
            return result != default
                ? new HiddenSubset(result.positions, result.values)
                : default;
        }

        public static (IEnumerable<Position> positions, IEnumerable<InputValue> values) HiddenSubset(IGrid input, int depth)
        {
            foreach( var house in Position.Houses )
            {
                var result = HiddenSubsetStep(input, house, new HashSet<Position>(), new List<InputValue>(), depth);
                if(  result != default )
                {
                    return result;
                }
            }
            return default;
        }

        public static (IEnumerable<Position> positions, IEnumerable<InputValue> values) HiddenSubsetStep(
            IGrid input,
            IEnumerable<Position> house,
            HashSet<Position> positions,
            List<InputValue> values,
            int depth)
        {
            if (positions.Count > depth || values.Count > depth)
            {
                return default;
            }

            if (positions.Count == depth && values.Count == depth)
            {
                if (positions.Any(pos => InputValue.NonEmpty.Except(values).Any(value => input.HasCandidate(pos, value))))
                {
                    return (positions, values);
                }
            }

            foreach( var value in InputValue.NonEmpty.Except(values) )
            {
                var positionsWithCandidate = house
                    .Where(pos => input.HasCandidate(pos, value));

                var count = positionsWithCandidate.Count();
                if (count > depth || count == 0)
                {
                    continue;
                }

                var positionsNew = positions.ToHashSet();
                foreach( var pos in positionsWithCandidate )
                {
                    positionsNew.Add(pos);
                }
                var valuesNew = values.ToList();
                valuesNew.Add(value);
                var result = HiddenSubsetStep(input, house, positionsNew, valuesNew, depth);
                if ( result != default)
                {
                    return result;
                }
            }

            return default;
        }
        #endregion
    }
}
