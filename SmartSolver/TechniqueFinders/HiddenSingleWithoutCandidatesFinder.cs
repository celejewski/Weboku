using Core.Data;
using SmartSolver.SolvingTechniques;
using System.Collections.Generic;
using System.Data;
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
            foreach( var value in InputValue.NonEmpty )
            {
                var rowContainsValue = new bool[9];
                var colContainsValue = new bool[9];
                var blockContainsValue = new bool[9];
                var positionsCanPutValue = new bool[9, 9];

                foreach( var pos in Position.All )
                {
                    if (grid.GetValue(pos) == value)
                    {
                        colContainsValue[pos.x] = true;
                        rowContainsValue[pos.y] = true;
                        blockContainsValue[pos.block] = true;
                    }
                }

                foreach( var pos in Position.All )
                {
                    positionsCanPutValue[pos.x, pos.y] =
                        !grid.HasValue(pos)
                        && !colContainsValue[pos.x]
                        && !rowContainsValue[pos.y]
                        && !blockContainsValue[pos.block];
                }

                for( int i = 0; i < 9; i++ )
                {
                    if( !rowContainsValue[i] && Position.Rows[i].Count(pos => positionsCanPutValue[pos.x, pos.y]) == 1 )
                    {
                        var pos = Position.Rows[i].First(pos => positionsCanPutValue[pos.x, pos.y]);
                        yield return _factory.HiddenSingle(pos, value);
                    }
                    if( !colContainsValue[i] && Position.Cols[i].Count(pos => positionsCanPutValue[pos.x, pos.y]) == 1 )
                    {
                        var pos = Position.Cols[i].First(pos => positionsCanPutValue[pos.x, pos.y]);
                        yield return _factory.HiddenSingle(pos, value);
                    }
                    if( !blockContainsValue[i] && Position.Blocks[i].Count(pos => positionsCanPutValue[pos.x, pos.y]) == 1 )
                    {
                        var pos = Position.Blocks[i].First(pos => positionsCanPutValue[pos.x, pos.y]);
                        yield return _factory.HiddenSingle(pos, value);
                    }
                }
            }
        }
    }
}
