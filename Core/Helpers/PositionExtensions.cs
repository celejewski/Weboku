using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Positions = System.Collections.Generic.IEnumerable<Core.Data.Position>;

namespace Core.Helpers
{
    public static class PositionExtensions
    {
        public static Positions WithValue(this Positions positions, IGrid grid)
        {
            foreach( var pos in positions )
            {
                if (grid.HasValue(pos))
                {
                    yield return pos;
                }
            }
        }

        public static Positions WithValue(this Positions positions, IGrid grid, InputValue value)
        {
            foreach( var pos in positions )
            {
                if( grid.GetValue(pos) == value )
                {
                    yield return pos;
                }
            }
        }


        public static Positions WithCandidate(this Positions positions, IGrid grid, InputValue value)
        {
            foreach( var pos in positions )
            {
                if( grid.HasCandidate(pos, value))
                {
                    yield return pos;
                }
            }
        }


    }
}
