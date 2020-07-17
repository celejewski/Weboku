using Core.Data;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UI.BlazorWASM.Hints.SolvingTechniques;
using System.Diagnostics;

namespace UI.BlazorWASM.Hints
{
    public class HintsSolver
    {
        private static readonly Func<IGrid, IDisplaySolvingTechniqueOld>[] _steps
            = new Func<IGrid, IDisplaySolvingTechniqueOld>[]
        {
            FullHouse,
            NakedSingle,
            HiddenSingle,
            LockedCandidatesPointing,
            LockedCandidatesClaiming,
            NakedPair,
            NakedTriple,
            HiddenPair,
            XWing,
            Skyscrapper,
            XYWing,
            NakedQuadruple,
            HiddenTriple
        };
        public static IDisplaySolvingTechniqueOld NextStep(IGrid input)
        {
            return _steps.Select(step => Benchmark(step, input))
                .FirstOrDefault(solvingTechnique => solvingTechnique != null);
        }

        private static IDisplaySolvingTechniqueOld Benchmark(Func<IGrid, IDisplaySolvingTechniqueOld> action, IGrid grid)
        {
            var stopwatch = Stopwatch.StartNew();
            var result = action(grid);
            stopwatch.Stop();
#if DEBUG
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms - {action.Method.Name}");
#endif
            return result;
        }

        #region Singles
        private static IDisplaySolvingTechniqueOld FullHouse(IGrid grid)
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
        private static IDisplaySolvingTechniqueOld NakedSingle(IGrid input)
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
        private static IDisplaySolvingTechniqueOld HiddenSingle(IGrid grid)
        {
            foreach( var positions in Position.Houses )
                foreach( var value in InputValue.NonEmpty )
                {
                    var positionsWithCandidate = positions.WithCandidate(grid, value);
                    if( positionsWithCandidate.Count() == 1 )
                    {
                        var pos = positionsWithCandidate.First();
                        return new HiddenSingle(pos, value);
                    }
                }
            return null;
        }
        #endregion

        #region Locked Candidates
        private static IDisplaySolvingTechniqueOld LockedCandidatesPointing(IGrid grid)
        {
            foreach( var value in InputValue.NonEmpty )
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
            return null;
        }

        private static IDisplaySolvingTechniqueOld LockedCandidatesClaiming(IGrid grid)
        {
            foreach( var value in InputValue.NonEmpty )
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
            return null;
        }
        #endregion

        #region Naked Subset
        private static IDisplaySolvingTechniqueOld NakedPair(IGrid grid)
        {
            var result = NakedSubset(grid, 2);
            return result != default
                ? new NakedPair(result.positions, result.values)
                : null;
        }

        private static IDisplaySolvingTechniqueOld NakedTriple(IGrid grid)
        {
            var result = NakedSubset(grid, 3);
            return result != default
                ? new NakedSubset(result.positions, result.values)
                : null;
        }

        private static IDisplaySolvingTechniqueOld NakedQuadruple(IGrid grid)
        {
            var result = NakedSubset(grid, 4);
            return result != default
                ? new NakedSubset(result.positions, result.values)
                : null;
        }

        private static (IEnumerable<Position> positions, IEnumerable<InputValue> values) NakedSubset(IGrid grid, int depth)
        {
            foreach( var house in Position.Houses )
            {
                var positionsInHouse = house
                    .WithoutValue(grid)
                    .Where(pos => grid.CandidatesCount(pos) <= depth);

                if( positionsInHouse.Count() < depth ) continue;

                var result = NakedSubsetStep(grid, new List<Position>(), new HashSet<InputValue>(), positionsInHouse, depth, 0);
                if( result != default ) return result;
            }
            return default;
        }

        private static (IEnumerable<Position> positions, IEnumerable<InputValue> values) NakedSubsetStep(
            IGrid grid,
            List<Position> positions,
            HashSet<InputValue> values,
            IEnumerable<Position> house,
            int depth,
            int index)
        {
            if( values.Count > depth ) return default;
            if( positions.Count == depth )
            {
                var positionsSeen = Position.GetOtherPositionsSeenBy(positions);
                return values.Any(value => positionsSeen.WithCandidate(grid, value).Any())
                    ? (positions, values)
                    : default;
            }

            foreach( var pos in house.Skip(index+1) )
            {
                index += 1;
                var positionsNew = new List<Position>(positions) { pos };
                var valuesNew = new HashSet<InputValue>(values);
                valuesNew.UnionWith(grid.GetCandidates(pos));

                var result = NakedSubsetStep(grid, positionsNew, valuesNew, house, depth, index);
                if( result != default ) return result;
            }
            return default;
        }
        #endregion

        #region Hidden Subset
        public static IDisplaySolvingTechniqueOld HiddenPair(IGrid input)
        {
            var result = HiddenSubset(input, 2);
            return result != default
                ? new HiddenPair(result.positions, result.values)
                : default;
        }

        public static IDisplaySolvingTechniqueOld HiddenTriple(IGrid input)
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
                if( result != default ) return result;
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
            if( positions.Count > depth ) return default;
            if( values.Count == depth )
            {
                var isAnyCandidateAnywhereToRemove = positions.Any(
                    pos => InputValue.NonEmpty.Except(values).Any(value => input.HasCandidate(pos, value)));
                return isAnyCandidateAnywhereToRemove
                    ? (positions, values)
                    : default;
            }

            foreach( var value in InputValue.NonEmpty.Except(values) )
            {
                var positionsWithCandidate = house.Where(pos => input.HasCandidate(pos, value));
                var count = positionsWithCandidate.Count();
                if( count > depth || count == 0 ) continue;

                var positionsNew = positions.ToHashSet();
                positionsNew.UnionWith(positionsWithCandidate);
                var valuesNew = new List<InputValue>(values) { value };
                var result = HiddenSubsetStep(input, house, positionsNew, valuesNew, depth);
                if( result != default ) return result;
            }
            return default;
        }
        #endregion

        #region Wings
        public static IDisplaySolvingTechniqueOld XWing(IGrid input)
        {
            foreach( var value in InputValue.NonEmpty )
            {
                foreach( var houses in new[] { Position.Rows, Position.Cols } )
                {
                    var filtered = houses.Select(
                        house => house.Where(pos => input.HasCandidate(pos, value)))
                        .Where(positions => positions.Count() == 2);

                    var housesCombined = new List<(IEnumerable<Position> first, IEnumerable<Position> second)>();
                    for( int i = 0; i < filtered.Count(); i++ )
                    {
                        for( int j = i+1; j < filtered.Count(); j++ )
                        {
                            var pair = (first: filtered.ElementAt(i), second: filtered.ElementAt(j));
                            housesCombined.Add(pair);
                        }
                    }

                    var housesCombinedAndFiltered = housesCombined.Where(pair => pair.first.All(pos1 => pair.second.Any(pos2 => pos1.x == pos2.x || pos1.y == pos2.y)));
                    if( housesCombinedAndFiltered.Any() )
                    {
                        var firstHouse = housesCombinedAndFiltered.First().first;
                        var secondHouse = housesCombinedAndFiltered.First().second;

                        var positions = firstHouse.Concat(secondHouse);
                        var positionsSeen = positions.SelectMany(pos => Position.Cols[pos.x].Concat(Position.Rows[pos.y]));
                        var positionsToRemove = positionsSeen.Except(positions)
                            .Where(pos => input.HasCandidate(pos, value));

                        if (positionsToRemove.Any()) return new XWing(value, positions, positionsToRemove, Position.GetHouse(firstHouse));
                        
                    };
                }
            }
            return default;
        }

        public static IDisplaySolvingTechniqueOld XYWing(IGrid input)
        {
            var pairPositions = Position.All.Where(pos => input.CandidatesCount(pos) == 2);

            foreach( var pivot in pairPositions )
            {
                var seenBy = GridHelper.GetCoordsWhichCanSee(pivot)
                    .Where(pos => input.CandidatesCount(pos) == 2);

                var candidate1 = input.GetCandidates(pivot).First();
                var candidate2 = input.GetCandidates(pivot).Last(); ;

                var seenWithCandidate1 = seenBy.Where(pos => input.HasCandidate(pos, candidate1) && !input.HasCandidate(pos, candidate2));
                var seenWithCandidate2 = seenBy.Where(pos => input.HasCandidate(pos, candidate2) && !input.HasCandidate(pos, candidate1));

                foreach( var pos1 in seenWithCandidate1.Where(pos => !pos.Equals(pivot)) )
                {
                    foreach( var pos2 in seenWithCandidate2.Where(pos => !pos.Equals(pivot) && !pos.Equals(pos1)) )
                    {
                        if( pos1.IsSharingHouseWith(pos2) ) continue;

                        var candidates1 = input.GetCandidates(pos1);
                        var candidates2 = input.GetCandidates(pos2);
                        var sharedValue = candidates1.FirstOrDefault(value => candidates2.Contains(value));
                        if( sharedValue == InputValue.Empty ) continue;

                        var positionsToRemoveFrom = Position.GetOtherPositionsSeenBy(pos1, pos2)
                            .Where(pos => input.HasCandidate(pos, sharedValue));
                        if (positionsToRemoveFrom.Any())
                        {
                            return new XYWing(pivot, pos1, pos2, candidate1, candidate2, positionsToRemoveFrom, sharedValue);
                        }
                    }
                }
            }

            return default;
        }
        #endregion

        private static IDisplaySolvingTechniqueOld Skyscrapper(IGrid input)
        {
            foreach( var value in InputValue.NonEmpty )
            {
                foreach( var houses in new[] { Position.Rows, Position.Cols } )
                {
                    var housesWithCandidate = houses.Select(house => house.Where(pos => input.HasCandidate(pos, value)));
                    var housesWithTwoCells = housesWithCandidate.Where(house => house.Count() == 2);

                    var housesPaired = housesWithTwoCells.SelectMany(
                        (house1, i) => housesWithTwoCells.Skip(i + 1).Select(house2 => (house1, house2)));

                    var possibleSkyscrappers = housesPaired
                        .Where(houses => 
                        houses.house1.Any(pos1 => houses.house2.Any(pos2 => pos1.x == pos2.x || pos1.y == pos2.y))
                        && houses.house1.Concat(houses.house2).Select(pos => pos.block).Distinct().Count() == 4
                        );

                    foreach( var (house1, house2) in possibleSkyscrappers )
                    {
                        var pos1 = house1.FirstOrDefault(pos1 => house2.All(pos2 => !pos1.IsSharingHouseWith(pos2)));
                        var pos2 = house2.FirstOrDefault(pos2 => house1.All(pos1 => !pos1.IsSharingHouseWith(pos2)));

                        var positionsToRemoveFrom = Position.GetOtherPositionsSeenBy(pos1, pos2)
                            .Where(pos => input.HasCandidate(pos, value));

                        if( positionsToRemoveFrom.Any() )
                        {
                            var base1 = house1.First(pos => !pos.Equals(pos1));
                            var base2 = house2.First(pos => !pos.Equals(pos2));
                            return new Skyscrapper(base1, base2, pos1, pos2, value);
                        }                        
                    }
                }
            }

            return default;
        }
    }
}
