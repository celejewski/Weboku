using Core.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using UI.BlazorWASM.Helpers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints
{
    public static class HintsHelper
    {
        public static IEnumerable<Position> GetPositionsInRow(int y)
        {

            for( int x = 0; x < 9; x++ )
            {
                yield return new Position(x, y);
            }
        }
        public static IEnumerable<Position> GetPositionsInCol(int x)
        {

            for( int y = 0; y < 9; y++ )
            {
                yield return new Position(x, y);
            }
        }

        public static IEnumerable<Position> GetPositionsInBlock(int block)
        {
            var x = (block % 3) * 3;
            var y = (block / 3) * 3;
            return GetPositionsInBlock(new Position(x, y));
        }

        public static IEnumerable<Position> GetPositionsInBlock(Position position)
        {
            var startingX = position.X / 3 * 3;
            var startingY = position.Y / 3 * 3;
            for( int offsetX = 0; offsetX < 3; offsetX++ )
            {
                for( int offsetY = 0; offsetY < 3; offsetY++ )
                {
                    yield return new Position(startingX + offsetX, startingY + offsetY);
                }
            }
        }

        public static IEnumerable<Position> GetPositionsInHouse(Position position, House house)
        {
            return house switch
            {
                House.None => Enumerable.Empty<Position>(),
                House.Row => GetPositionsInRow(position.Y),
                House.Col => GetPositionsInCol(position.X),
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
            return positions[0].X == positions[1].X ? House.Col : House.Row;
        }

        public static string Format(House house)
        {
            return house switch
            {
                House.None => "none",
                House.Row => "row",
                House.Col => "column",
                House.Block => "block",
                _ => throw new ArgumentException("House not supported by HintHelper.Format")
            };
        }

        public static string Format(House house, Position position)
        {
            return house switch
            {
                House.Row => $"row {position.Y + 1}",
                House.Col => $"column {position.X + 1}",
                House.Block => $"block {position.Block + 1}",
                _ => "none"
            };
        }

        public static IEnumerable<House> GetHouses(IEnumerable<Position> positions)
        {
            var first = positions.First();

            if( positions.All(pos => pos.X == first.X) )
            {
                yield return House.Col;
            }
            if( positions.All(pos => pos.Y == first.Y) )
            {
                yield return House.Row;
            }
            if( positions.All(pos => pos.Block == first.Block) )
            {
                yield return House.Block;
            }
        }
    }
}
