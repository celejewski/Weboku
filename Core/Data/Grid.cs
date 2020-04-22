using System;
using System.Text;

namespace Core.Data
{
    public class Grid : ICloneable
    {
        private string[,] _grid = new string[9, 9];

        public Grid() { }
        public Grid(string input)
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    var cell = input[y * 9 + x].ToString();
                    if (cell == "0" || cell == ",")
                    {
                        cell = null;
                    }
                    _grid[x, y] = cell;
                }
            }
        }

        public void SetValue(int x, int y, string value)
        {
            _grid[x, y] = value;
        }

        public string GetValue(int x, int y)
        {
            return _grid[x, y];
        }

        public bool IsLegalValue(int x, int y, string value)
        {
            if (value == null)
            {
                return true;
            }

            return IsLegalValueForCol(x, y, value)
                && IsLegalValueForRow(x, y, value)
                && IsLegalForBlock(x, y, value);
        }

        private bool IsLegalValueForCol(int x, int y, string value)
        {
            for( int row = 0; row < 9; row++ )
            {
                if (row == y)
                {
                    continue;
                }

                if( _grid[x, row] == value )
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsLegalValueForRow(int x, int y, string value)
        {
            for( int col = 0; col < 9; col++ )
            {
                if (col == x)
                {
                    continue;
                }

                if( _grid[col, y] == value )
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsLegalForBlock(int x, int y, string value)
        {
            var blockX = (x / 3) * 3;
            var blockY = (y / 3) * 3;
            for( int offsetX = 0; offsetX < 3; offsetX++ )
            {
                for( int offsetY = 0; offsetY < 3; offsetY++ )
                {
                    var targetX = blockX + offsetX;
                    var targetY = blockY + offsetY;
                    if (targetX == x && targetY == y)
                    {
                        continue;
                    }

                    if( _grid[targetX, targetY] == value )
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for( int y = 0; y < 9; y++ )
            {
                for( int x = 0; x < 9; x++ )
                {
                    sb.Append(_grid[x, y] ?? "0");
                }
            }

            return sb.ToString();
        }

        public object Clone() => new Grid(this.ToString());
    }
}
