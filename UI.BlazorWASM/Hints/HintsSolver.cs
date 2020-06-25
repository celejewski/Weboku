using Core.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UI.BlazorWASM.Hints.SolvingTechniques;

namespace UI.BlazorWASM.Hints
{
    public class HintsSolver
    {
        public ISolvingTechnique Solve(IGrid input)
        {
            var grid = input.Clone();

            GridHelper.SetAllLegalCandidates(grid);
            return NextStep(grid)(grid);
        }

        private static readonly Func<IGrid, ISolvingTechnique>[] _steps = new Func<IGrid, ISolvingTechnique>[] {
            FullHouse,
            NakedSingle,
            HiddenSingle,
        };
        private static Func<IGrid, ISolvingTechnique> NextStep(IGrid input)
        {
            foreach( var step in _steps )
            {
                var output = step(input);
                if( output != null )
                {
                    return step;
                }
            }
            return null;
        }
        private static ISolvingTechnique FullHouse(IGrid input)
        {
            foreach( var indexes in GetIndexesFromAllHouses() )
            {
                if( indexes.Count(index => input.HasValue(index)) == 8 )
                {
                    var value = InputValue.NonEmpty.First(
                        value => indexes.All(index => input.GetValue(index) != value));
                    var pos = indexes.First(index => !input.HasValue(index));

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
            foreach( var indexes in GetIndexesFromAllHouses() )
            {
                foreach( var value in InputValue.NonEmpty )
                {
                    if( indexes.Count(index => input.HasCandidate(index, value)) == 1 )
                    {
                        var pos = indexes.First(index => input.HasCandidate(index, value));
                        return new HiddenSingle(pos, value);
                    }
                }
            }
            return null;
        }
        static HintsSolver()
        {
            _indexesFromAllHouses.AddRange(Position.Blocks
                .Concat(Position.Cols)
                .Concat(Position.Rows));
        }
        private readonly static List<IEnumerable<Position>> _indexesFromAllHouses = new List<IEnumerable<Position>>();
        private static IEnumerable<IEnumerable<Position>> GetIndexesFromAllHouses() => _indexesFromAllHouses;

    }
}
