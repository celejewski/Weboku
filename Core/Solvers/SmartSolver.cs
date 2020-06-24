using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Solvers
{
    public class SmartSolver : ISolver
    {
        public IGrid Solve(IGrid input)
        {
            var grid = input.Clone();

            while (true)
            {
                GridHelper.SetAllLegalCandidates(grid);
                var nextStep = NextStep(grid);
                if (nextStep == null)
                {
                    return grid;
                }
                grid = nextStep(grid);
            }
        }

        private static readonly Func<IGrid, IGrid>[] _steps = new Func<IGrid, IGrid>[] { 
            FullHouse,
            NakedSingle,
            HiddenSingle,
        };
        private static Func<IGrid, IGrid> NextStep(IGrid input)
        {
            foreach( var step in _steps )
            {
                var output = step(input);
                if (output != null)
                {
                    return step;
                }
            }
            return null;
        }
        private static IGrid FullHouse(IGrid input)
        {
            var grid = input.Clone();
            foreach( var indexes in GetIndexesFromAllHouses() )
            {
                if (indexes.Count(index => grid.HasValue(index)) == 8)
                {
                    var value = InputValue.NonEmpty.First(
                        value => indexes.All(index => grid.GetValue(index) != value));
                    var pos = indexes.First(index => !grid.HasValue(index));

                    grid.SetValue(pos, value);
                    return grid;
                }
            }
            return null;
        }

        private static IGrid NakedSingle(IGrid input)
        {
            foreach( var pos in Position.All )
            {
                if (!input.HasValue(pos)
                    && input.GetCandidatesCount(pos) == 1)
                {
                    var grid = input.Clone();
                    var value = InputValue.NonEmpty.First(value => grid.HasCandidate(pos, value));
                    grid.SetValue(pos, value);

                    return grid;
                }
            }
            return null;
        }
        
        private static IGrid HiddenSingle(IGrid input)
        {
            foreach( var indexes in GetIndexesFromAllHouses() )
            {
                foreach( var value in InputValue.NonEmpty )
                {
                    var isHiddenSingle = indexes.Count(index => input.HasCandidate(index, value)) == 1;
                    if (!isHiddenSingle)
                    {
                        continue;
                    }
                    var first = indexes.First(index => input.HasCandidate(index, value));
                    var grid = input.Clone();
                    grid.SetValue(first, value);
                    return grid;
                }
            }
            return null;
        }

        static SmartSolver()
        {

            _indexesFromAllHouses.AddRange(Position.Blocks
                .Concat(Position.Cols)
                .Concat(Position.Rows));
        }
        private readonly static List<IEnumerable<Position>> _indexesFromAllHouses = new List<IEnumerable<Position>>();
        private static IEnumerable<IEnumerable<Position>> GetIndexesFromAllHouses() => _indexesFromAllHouses;
    }
}
