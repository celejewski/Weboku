using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data
{
    public class GridHelper
    {
        private static readonly IReadOnlyList<Position>[,] _indexesWhichCanSee = new IReadOnlyList<Position>[9, 9];

        public static IReadOnlyList<Position> GetCoordsWhichCanSee(Position pos)
        {
            return _indexesWhichCanSee[pos.x, pos.y] ??= CalculateIndexesWhichCanSee(pos);
        }

        private static IReadOnlyList<Position> CalculateIndexesWhichCanSee(Position pos)
        {
            return Position.Cols[pos.x]
                .Concat(Position.Rows[pos.y])
                .Concat(Position.Blocks[pos.Block])
                .ToArray();
        }

        public static bool IsLegal(Position pos, InputValue value, IGrid grid)
        {
            return value == InputValue.Empty
                || GetCoordsWhichCanSee(pos)
                .Where(coords => !pos.Equals(coords))
                .All(coords => grid.GetValue(coords) != value);
        }

        public static void SetAllLegalCandidates(IGrid grid)
        {
            grid.FillCandidates();
            foreach( var pos in Position.All )
            {
                foreach( var value in InputValue.NonEmpty )
                {
                    if( !IsLegal(pos, value, grid) )
                    {
                        grid.RemoveCandidate(pos, value);
                    }
                }
            }
        }

        public static void RemoveCandidatesSeenBy(IGrid grid, Position pos)
        {
            var value = grid.GetValue(pos);
            if (value == InputValue.Empty)
            {
                return;
            }

            foreach( var coords in GridHelper.GetCoordsWhichCanSee(pos) )
            {
                grid.RemoveCandidate(coords, value);
            }
        }
    }
}
