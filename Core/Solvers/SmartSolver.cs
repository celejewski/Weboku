using Core.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace Core.Solvers
{
    public class SmartSolver : ISolver
    {
        public IGrid Solve(IGrid input)
        {
            var grid = input.Clone();

            while (true)
            {
                var nextStep = NextStep(grid);
                if (nextStep == null)
                {
                    return grid;
                }
                grid = nextStep(grid);
            }
        }

        private static readonly Func<IGrid, IGrid>[] _steps = new Func<IGrid, IGrid>[] { 
            FullHouse
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
            var grid = input.Clone();
            foreach( var indexes in GetIndexesFromAllHouses() )
            {
                if (indexes.Count(index => input.HasValue(index.X, index.Y)) == 8)
                {
                    var value = GridHelper.Values.First(
                        value => indexes.All(index => input.GetValue(index.X, index.Y) != value));
                    var pos = indexes.First(index => !input.HasValue(index.X, index.Y));

                    grid.SetValue(pos.X, pos.Y, value);
                    return grid;
                }
            }
            return null;
        }

        private readonly static List<IEnumerable<Position>> _indexesFromAllHouses = new List<IEnumerable<Position>>();
        private static IEnumerable<IEnumerable<Position>> GetIndexesFromAllHouses()
        {
            if (_indexesFromAllHouses.Count > 0)
            {
                return _indexesFromAllHouses;
            }

            foreach( var pos in _allBlocks )
            {
                _indexesFromAllHouses.Add(GridHelper.GetIndexesFromBlock(pos.X, pos.Y));
            }

            foreach( var pos in _allCols )
            {
                _indexesFromAllHouses.Add(GridHelper.GetIndexesFromCol(pos.X));
            }

            foreach( var pos in _allRows)
            {
                _indexesFromAllHouses.Add(GridHelper.GetIndexesFromRow(pos.Y));
            }

            return _indexesFromAllHouses;
        }

        private readonly static Position[] _allBlocks = new Position[]
        {
            new Position(0, 0),
            new Position(0, 3),
            new Position(0, 6),
            new Position(3, 0),
            new Position(3, 3),
            new Position(3, 6),
            new Position(6, 0),
            new Position(6, 3),
            new Position(6, 6),
        };

        private readonly static Position[] _allRows = new Position[]
        {
            new Position(0, 0),
            new Position(0, 1),
            new Position(0, 2),
            new Position(0, 3),
            new Position(0, 4),
            new Position(0, 5),
            new Position(0, 6),
            new Position(0, 7),
            new Position(0, 8),
        };

        private readonly static Position[] _allCols = new Position[]
        {
            new Position(0, 0),
            new Position(1, 0),
            new Position(2, 0),
            new Position(3, 0),
            new Position(4, 0),
            new Position(5, 0),
            new Position(6, 0),
            new Position(7, 0),
            new Position(8, 0),
        };

    }
}
