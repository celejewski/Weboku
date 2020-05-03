using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data
{
    public class Grid : ICloneable
    {
        private readonly Cell[,] _cells;
        public ICell[,] Cells { get => _cells; }

        private readonly int[,] _grid = new int[9, 9];

        public Grid() 
        {
            _cells = new Cell[9, 9];
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    _cells[x, y] = new Cell
                    {
                        Col = x,
                        Row = y,
                        Block = (y / 3) * 3 + (x / 3),
                };
                }
            }
        }
        public Grid(string input) : this()
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    var cell = int.Parse(input[y * 9 + x].ToString());
                    SetValue(x, y, cell);
                    if (cell != 0)
                    {
                        _cells[x, y].IsGiven = true;
                    }
                }
            }
        }

        public void SetValue(int x, int y, int value)
        {
            _grid[x, y] = value;
            _cells[x, y].Input.Value = value;
            bool isLegal = IsLegalValue(x, y, value);
            _cells[x, y].Input.IsLegal = isLegal;

            if( value != 0 && isLegal )
            {
                foreach( var cell in GetCellsWhichCanSee(x, y) )
                {
                    var cellInput = cell.Candidates.FirstOrDefault(ci => ci.Value == value);
                    if( cellInput != null )
                    {
                        cell.Candidates.Remove(cellInput);
                    }
                }
            }
        }

        public int GetValue(int x, int y)
        {
            return _cells[x, y].Input.Value;
        }

        private IEnumerable<Cell> GetCellsWhichCanSee(int x, int y)
        {
            var indexes = GetIndexesFromCol(x)
                .Concat(GetIndexesFromRow(y))
                .Concat(GetIndexesFromBlock(x, y));

            return indexes.Select(index => _cells[index.x, index.y]);
        }
        public IEnumerable<(int x, int y)> GetIndexesFromRow(int y)
        {
            for( int col = 0; col < 9; col++ )
            {
                yield return (col, y);
            }
        }

        public IEnumerable<(int x, int y)> GetIndexesFromCol(int x)
        {
            for( int row = 0; row < 9; row++ )
            {
                yield return (x, row);
            }
        }

        public IEnumerable<(int x, int y)> GetIndexesFromBlock(int x, int y)
        {
            var blockX = (x / 3) * 3;
            var blockY = (y / 3) * 3;
            for( int offsetX = 0; offsetX < 3; offsetX++ )
            {
                for( int offsetY = 0; offsetY < 3; offsetY++ )
                {
                    var targetX = blockX + offsetX;
                    var targetY = blockY + offsetY;
                    yield return (targetX, targetY);
                }
            }
        }

        public bool IsLegalValue(int x, int y, int value)
        {
            if( value == 0 )
            {
                return true;
            }

            return IsLegalValueForCol(x, y, value)
                && IsLegalValueForRow(x, y, value)
                && IsLegalForBlock(x, y, value);
        }

        private bool IsLegalValueFor(int x, int y, int value, IEnumerable<(int x, int y)> indexesToCheck)
        {
            return value == 0 || indexesToCheck
                .Where(index => !(index.x == x && index.y == y))
                .All(index => _grid[index.x, index.y] != value);
        }

        private bool IsLegalValueForCol(int x, int y, int value)
        {
            return IsLegalValueFor(x, y, value, GetIndexesFromCol(x));
        }

        private bool IsLegalValueForRow(int x, int y, int value)
        {
            return IsLegalValueFor(x, y, value, GetIndexesFromRow(y));
        }

        private bool IsLegalForBlock(int x, int y, int value)
        {
            return IsLegalValueFor(x, y, value, GetIndexesFromBlock(x, y));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for( int y = 0; y < 9; y++ )
            {
                for( int x = 0; x < 9; x++ )
                {
                    sb.Append(_grid[x, y]);
                }
            }

            return sb.ToString();
        }

        public object Clone() => new Grid(this.ToString());
    }
}
