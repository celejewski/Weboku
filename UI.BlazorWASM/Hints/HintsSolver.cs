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
            LockedCandidatesClaimingRow,
            LockedCandidatesClaimingCol,
            NakedPair,
            HiddenSubset,
            NakedSubset,
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
        private static ISolvingTechnique HiddenSingle(IGrid input)
        {
            foreach( var indexes in Position.Houses )
            {
                foreach( var value in InputValue.NonEmpty )
                {
                    var positionsWithCandidate = indexes.WithCandidate(input, value);
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
        private static ISolvingTechnique LockedCandidatesPointing(IGrid input)
        {
            foreach( var value in InputValue.NonEmpty )
            {
                foreach( var block in Position.Blocks )
                {
                    var positionsInBlock = block.Where(pos => input.HasCandidate(pos, value));

                    var count = positionsInBlock.Count();
                    if( count != 2 && count != 3 )
                    {
                        continue;
                    }

                    var first = positionsInBlock.First();
                    if( AreInRow(positionsInBlock) )
                    {
                        var positionsInRow = Position.Rows[first.y]
                            .Where(pos => input.HasCandidate(pos, value));

                        if( positionsInRow.Count() > count )
                        {
                            var positionsToRemove = positionsInRow.Except(positionsInBlock);
                            return new LockedCandidatesPointing(first.block, value, positionsToRemove);
                        }
                    }

                    if( AreInCol(positionsInBlock) )
                    {
                        var positionsInCol = Position.Cols[first.x]
                            .Where(pos => input.HasCandidate(pos, value));

                        if( positionsInCol.Count() > count )
                        {
                            var positionsToRemove = positionsInCol.Except(positionsInBlock);
                            return new LockedCandidatesPointing(first.block, value, positionsToRemove);
                        }
                    }
                }
            }
            return null;
        }

        private static ISolvingTechnique LockedCandidatesClaimingRow(IGrid grid)
        {
            foreach( var value in InputValue.NonEmpty )
            {
                foreach( var row in Position.Rows )
                {
                    var positionsInRow = row.Where(pos => grid.HasCandidate(pos, value));

                    if( !positionsInRow.Any() )
                    {
                        continue;
                    }

                    var first = positionsInRow.First();
                    if( positionsInRow.Any(pos => pos.block != first.block) )
                    {
                        continue;
                    }

                    var positionsInBlock = Position.Blocks[first.block]
                        .Where(pos => grid.HasCandidate(pos, value));

                    var positionsToRemove = positionsInBlock.Except(positionsInRow);
                    if( positionsToRemove.Any() )
                    {
                        return new LockedCandidatesClaiming(value, positionsToRemove, House.Row);
                    }
                }
            }
            return null;
        }

        private static ISolvingTechnique LockedCandidatesClaimingCol(IGrid grid)
        {
            foreach( var value in InputValue.NonEmpty )
            {
                foreach( var col in Position.Cols )
                {
                    var positionsInCol = col.Where(pos => grid.HasCandidate(pos, value));
                    if( !positionsInCol.Any() )
                    {
                        continue;
                    }

                    var first = positionsInCol.First();
                    if( positionsInCol.Any(pos => pos.block != first.block) )
                    {
                        continue;
                    }

                    var positionsInBlock = Position.Blocks[first.block]
                        .Where(pos => grid.HasCandidate(pos, value));

                    var positionsToRemove = positionsInBlock.Except(positionsInCol);
                    if( positionsToRemove.Any() )
                    {
                        return new LockedCandidatesClaiming(value, positionsToRemove, House.Col);
                    }
                }
            }
            return null;
        }

        private static bool AreInRow(IEnumerable<Position> positons)
        {
            return positons.Any()
                && positons.All(pos => pos.y == positons.First().y);
        }
        private static bool AreInCol(IEnumerable<Position> positons)
        {
            return positons.Any()
                && positons.All(pos => pos.x == positons.First().x);
        }
        #endregion

        #region Naked Subset
        private static ISolvingTechnique NakedPair(IGrid input)
        {
            foreach( var house in Position.Houses )
            {
                var filteredPositions = house.Where(pos => input.CandidatesCount(pos) == 2);
                foreach( var pos1 in filteredPositions )
                {
                    var candidates = InputValue.NonEmpty.Where(value => input.HasCandidate(pos1, value));
                    if( !candidates.Any() )
                    {
                        continue;
                    }
                    var filtered = filteredPositions.Where(pos => candidates.All(value => input.HasCandidate(pos, value)));
                    if( filtered.Count() == 2 )
                    {
                        var positionsInHouses = HintsHelper.GetHouses(filtered)
                            .SelectMany(house => HintsHelper.GetPositionsInHouse(pos1, house));

                        var positionsToRemoveFrom = positionsInHouses
                            .Where(pos => candidates.Any(value => input.HasCandidate(pos, value)))
                            .Except(filtered);
                        if( positionsToRemoveFrom.Any() )
                        {
                            return new NakedPair(filtered, candidates);
                        }
                    }
                }
            }

            return null;
        }

        private static ISolvingTechnique NakedSubset(IGrid input)
        {
            for( int depth = 2; depth < 5; depth++ )
            {
                foreach( var house in Position.Houses )
                {
                    if( NakedSubsetStep(input, new List<Position>(), new HashSet<InputValue>(), house, depth) is NakedSubset subset )
                    {
                        return subset;
                    }
                }
            }
            return null;
        }

        private static ISolvingTechnique NakedSubsetStep(
            IGrid input,
            List<Position> positions,
            HashSet<InputValue> values,
            IEnumerable<Position> house,
            int depth)
        {
            if( values.Count > depth )
            {
                return null;
            }

            if( positions.Count == depth )
            {
                var first = positions.First();
                var positionsSeenBy = HintsHelper.GetHouses(positions)
                    .SelectMany(house => HintsHelper.GetPositionsInHouse(first, house))
                    .Except(positions);

                if( values.Any(value => positionsSeenBy.Any(pos => input.HasCandidate(pos, value))))
                {
                    return new NakedSubset(positions, values);
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

                if( NakedSubsetStep(input, positionsNew, valuesNew, house, depth) is NakedSubset subset )
                {
                    return subset;
                }
            }
            return null;
        }
        #endregion

        #region Hidden Subset
        public static ISolvingTechnique HiddenSubset(IGrid input)
        {
            for( int depth = 2; depth < 5; depth++ )
            {
                foreach( var house in Position.Houses )
                {
                    if( HiddenSubsetStep(input, house, new HashSet<Position>(), new List<InputValue>(), depth) is HiddenSubset subset )
                    {
                        return subset;
                    }
                }
            }
            return null;
        }

        public static ISolvingTechnique HiddenSubsetStep(
            IGrid input,
            IEnumerable<Position> house,
            HashSet<Position> positions,
            List<InputValue> values,
            int depth)
        {
            if (positions.Count > depth || values.Count > depth)
            {
                return null;
            }

            if (positions.Count == depth && values.Count == depth)
            {
                if (positions.Any(pos => InputValue.NonEmpty.Except(values).Any(value => input.HasCandidate(pos, value))))
                {
                    return new HiddenSubset(positions, values);
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
                if (HiddenSubsetStep(input, house, positionsNew, valuesNew, depth) is HiddenSubset output)
                {
                    return output;
                }
            }

            return null;
        }
        #endregion
    }
}
