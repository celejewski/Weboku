using Core.Data;
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
        };
        private readonly static List<IEnumerable<Position>> _indexesFromAllHouses
            = new List<IEnumerable<Position>>();
        static HintsSolver()
        {
            _indexesFromAllHouses.AddRange(Position.Blocks
                .Concat(Position.Cols)
                .Concat(Position.Rows));
        }
        public static ISolvingTechnique NextStep(IGrid input)
        {
            return _steps.Select(step => step(input))
                .FirstOrDefault(solvingTechnique => solvingTechnique != null)
                ?? new NotFound();
        }

        #region Singles
        private static ISolvingTechnique FullHouse(IGrid input)
        {
            foreach( var indexes in _indexesFromAllHouses )
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
            foreach( var indexes in _indexesFromAllHouses )
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

    }
}
