using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UI.BlazorWASM.Hints
{
    public static class HintsHelper
    {
        public static IEnumerable<Position> GetPositionsInRow(int y)
        {
            return Position.Rows[y];
        }
        public static IEnumerable<Position> GetPositionsInCol(int x)
        {
            return Position.Cols[x];
        }

        public static IEnumerable<Position> GetPositionsInBlock(int block)
        {
            return Position.Blocks[block];
        }

        public static IEnumerable<Position> GetPositionsInBlock(Position position)
        {
            return Position.Blocks[position.block];
        }

        public static IEnumerable<Position> GetPositionsInHouse(Position position, House house)
        {
            return house switch
            {
                House.None => Enumerable.Empty<Position>(),
                House.Row => GetPositionsInRow(position.y),
                House.Col => GetPositionsInCol(position.x),
                House.Block => GetPositionsInBlock(position),
                _ => throw new ArgumentException("Unknown house"),
            };
        }

        public static House FindHouse(Position position, Predicate<IEnumerable<Position>> predicate)
        {
            foreach( var house in new[] { House.Row, House.Col, House.Block } )
            {
                if( predicate(GetPositionsInHouse(position, house)) )
                {
                    return house;
                }
            }
            return House.None;
        }

        public static House RowOrCol(params Position[] positions)
        {
            return positions[0].x == positions[1].x ? House.Col : House.Row;
        }

        public static IEnumerable<House> GetHouses(IEnumerable<Position> positions)
        {
            var first = positions.First();

            if( positions.All(pos => pos.x == first.x) )
            {
                yield return House.Col;
            }
            if( positions.All(pos => pos.y == first.y) )
            {
                yield return House.Row;
            }
            if( positions.All(pos => pos.block == first.block) )
            {
                yield return House.Block;
            }
        }
    }
}
