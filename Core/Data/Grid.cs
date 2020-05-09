using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data
{
    public class Grid : ICloneable, IGrid
    {
        private readonly Cell[,] _cells;
        public ICell[,] Cells { get => _cells; }
        private readonly IList<Cell>[,] _cellsWhichCanSee = new IList<Cell>[9, 9];

        public Grid()
        {
            _cells = new Cell[9, 9];
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    _cells[x, y] = new Cell
                    {
                        X = x,
                        Y = y,
                        Block = (y / 3) * 3 + (x / 3),
                    };
                }
            }
        }

        public void FillAllCandidates()
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    for( int i = 1; i < 10; i++ )
                    {
                        var cell = _cells[x, y];
                        if( !cell.Candidates.ContainsKey(i) )
                        {
                            cell.Candidates.Add(i, new CellInput { Value = i, IsLegal = true });
                        }
                    }
                }
            }

            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    RemoveCandidateSeenBy(x, y);
                }
            }
        }

        public void SetGiven(int x, int y, int value)
        {
            SetValue(x, y, value);
            _cells[x, y].IsGiven = true;
        }

        public void SetValue(int x, int y, int value)
        {
            var cell = _cells[x, y];
            cell.Input.Value = value;
            cell.Input.IsLegal = IsLegalValue(x, y, value);
            RemoveCandidateSeenBy(x, y);
        }

        private void RemoveCandidateSeenBy(int x, int y)
        {
            var cell = _cells[x, y];
            var value = cell.Input.Value;
            if( value != 0 && cell.Input.IsLegal )
            {
                var seenCells = GetCellsWhichCanSee(x, y);
                for( int i = 0; i < seenCells.Count; i++ )
                {
                    var seenCell = seenCells[i];
                    if( seenCell.Candidates.ContainsKey(value) )
                    {
                        seenCell.Candidates.Remove(value);
                    }
                }
            }
        }

        public void ToggleCandidate(int x, int y, int value)
        {
            var cell = _cells[x, y];
            if( !cell.Candidates.ContainsKey(value) )
            {
                cell.Candidates.Add(value, new CellInput
                {
                    Value = value,
                    IsLegal = IsLegalValue(x, y, value)
                });
            }
            else
            {
                cell.Candidates.Remove(value);
            }
        }

        public int GetValue(int x, int y)
        {
            return _cells[x, y].Input.Value;
        }

        private IList<Cell> CalculateCellsWhichCanSee(int x, int y)
        {
            var indexes = GetIndexesFromCol(x)
                .Concat(GetIndexesFromRow(y))
                .Concat(GetIndexesFromBlock(x, y));

            return indexes
                .Where(index => !(index.x == x && index.y == y))
                .Select(index => _cells[index.x, index.y])
                .ToArray();
        }

        private IList<Cell> GetCellsWhichCanSee(int x, int y)
        {
            if( _cellsWhichCanSee[x, y] == null )
            {
                _cellsWhichCanSee[x, y] = CalculateCellsWhichCanSee(x, y);
            }
            return _cellsWhichCanSee[x, y];
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
            return IsLegalValueFor(value, CalculateCellsWhichCanSee(x, y));
        }

        private bool IsLegalValueFor(int value, IList<Cell> cellsToCheck)
        {
            for( int i = 0; i < cellsToCheck.Count; i++ )
            {
                if( cellsToCheck[i].Input.Value == value )
                {
                    return false;
                }
            }
            return true;
        }

        public object Clone()
        {
            var clone = new Grid();
            clone.AssignFrom(this);
            return clone;
        }

        public void AssignFrom(IGrid source)
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    var from = source.Cells[x, y];
                    var to = this._cells[x, y];

                    to.IsGiven = from.IsGiven;
                    to.Input.Value = from.Input.Value;
                    to.Input.IsLegal = from.Input.IsLegal;

                    to.Candidates.Clear();
                    foreach( var candidate in from.Candidates )
                    {
                        to.Candidates.Add(candidate.Key, (CellInput) ((CellInput) candidate.Value).Clone());
                    }
                }
            }
        }
    }
}
