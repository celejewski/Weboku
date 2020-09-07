using Core.Data;
using Core.Validators;
using System.Collections.Generic;
using System.Linq;

namespace Core.Solvers
{
    public class BruteForceSolver : BaseSolver
    {
        private static readonly IDictionary<int, IGrid> _solved = new Dictionary<int, IGrid>();

        private int GetGivensHashcode(IGrid grid)
        {
            int hashcode = 0;
            for( int i = 0; i < 81; i++ )
            {
                var pos = Position.All[i];
                hashcode ^= grid.GetIsGiven(pos)
                    ? grid.GetValue(pos) << (i % 25)
                    : 0;
            }
            return hashcode;
        }

        public override IGrid Solve(IGrid input)
        {
            ValidatorGrid.EnsureGridIsValid(input);

            var hashcode = GetGivensHashcode(input);
            if( !_solved.ContainsKey(hashcode) )
            {
                var grid = input.Clone();
                grid.FillAllLegalCandidates();
                _solved[hashcode] = SolveStep(grid);
            }
            return _solved[hashcode];
        }

        private IGrid SolveStep(IGrid input)
        {
            if( IsSolved(input) )
            {
                return input;
            }

            if( !CanBeSolved(input) )
            {
                return null;
            }

            var pos = GetNextPosition(input);
            foreach( var value in InputValue.NonEmpty.Where(value => input.HasCandidate(pos, value)) )
            {
                var grid = input.Clone();
                grid.SetValue(pos, value);

                if( SolveStep(grid) is Grid output )
                {
                    return output;
                }
            }
            return null;
        }

        private bool IsSolved(IGrid grid)
        {
            return Position.All.All(pos => grid.HasValue(pos));
        }

        private bool CanBeSolved(IGrid grid)
        {
            return Position.All.All(pos => grid.HasValue(pos) || grid.GetCandidates(pos).Count() > 0);
        }

        private Position GetNextPosition(IGrid grid)
        {
            return Position.All.Where(pos => !grid.HasValue(pos))
                .Aggregate((nextPos, pos) => grid.GetCandidates(pos).Count() < grid.GetCandidates(nextPos).Count() ? pos : nextPos);
        }
    }
}
