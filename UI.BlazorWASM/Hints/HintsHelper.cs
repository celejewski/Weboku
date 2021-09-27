using System;
using System.Collections.Generic;
using System.Linq;
using Core.Data;

namespace Weboku.UserInterface.Hints
{
    public static class HintsHelper
    {
        public static IEnumerable<Position> GetPositionsInHouse(Position position, House house)
        {
            return house switch
            {
                House.None => Enumerable.Empty<Position>(),
                House.Row => Position.Rows[position.y],
                House.Col => Position.Cols[position.x],
                House.Block => Position.Blocks[position.block],
                _ => throw new ArgumentException("Unknown house"),
            };
        }

        private static readonly IEnumerable<House> _houses = new[] {House.Block, House.Row, House.Col};

        public static House HouseFirstOrDefault(Position pos, Predicate<IEnumerable<Position>> predicate)
        {
            return _houses.FirstOrDefault(
                house => predicate(GetPositionsInHouse(pos, house))
            );
        }

        public static House RowOrCol(params Position[] positions)
        {
            return positions[0].x == positions[1].x ? House.Col : House.Row;
        }

        public static IEnumerable<House> GetHouses(IEnumerable<Position> positions)
        {
            var first = positions.First();

            if (positions.All(pos => pos.x == first.x))
            {
                yield return House.Col;
            }

            if (positions.All(pos => pos.y == first.y))
            {
                yield return House.Row;
            }

            if (positions.All(pos => pos.block == first.block))
            {
                yield return House.Block;
            }
        }
    }
}