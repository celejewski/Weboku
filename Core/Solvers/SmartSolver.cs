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
            foreach( var indexes in GetIndexesFromAllHouses() )
            {
                if (indexes.Count(index => input.HasValue(index)) == 8)
                {
                    var output = input.Clone();
                    var value = InputValue.NonEmpty.First(
                        value => indexes.All(index => output.GetValue(index) != value));
                    var pos = indexes.First(index => !output.HasValue(index));

                    output.SetValue(pos, value);
                    return output;
                }
            }
            return null;
        }
        private static IGrid NakedSingle(IGrid input)
        {
            foreach( var pos in Position.All )
            {
                if (!input.HasValue(pos)
                    && input.CandidatesCount(pos) == 1)
                {
                    var output = input.Clone();
                    var value = InputValue.NonEmpty.First(value => output.HasCandidate(pos, value));
                    output.SetValue(pos, value);

                    return output;
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
                    if (indexes.Count(index => input.HasCandidate(index, value)) == 1 )
                    {
                        var candidate = indexes.First(index => input.HasCandidate(index, value));
                        var output = input.Clone();
                        output.SetValue(candidate, value);
                        return output;
                    }
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
