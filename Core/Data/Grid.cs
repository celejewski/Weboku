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
        private IEnumerable<Cell>[,] _cellsWhichCanSee = new IEnumerable<Cell>[9, 9];

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

        public void FillAllCandidates()
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    for( int i = 1; i < 10; i++ )
                    {
                        _cells[x, y].Candidates.Add(new CellInput { Value = i, IsLegal = true });
                    }
                }
            }

            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    SetValue(x, y, _cells[x, y].Input.Value);
                }
            }
        }

        public Grid(string input) : this()
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    var value = int.Parse(input[y * 9 + x].ToString());
                    SetValue(x, y, value);
                    if (value != 0)
                    {
                        _cells[x, y].IsGiven = true;
                    }
                }
            }
        }

        public void SetValue(int x, int y, int value)
        {
            var cell = _cells[x, y];
            cell.Input.Value = value;
            bool isLegal = IsLegalValue(x, y, value);
            cell.Input.IsLegal = isLegal;

            if( value != 0 && isLegal )
            {
                foreach( var c in GetCellsWhichCanSee(x, y) )
                {
                    var cellInput = c.Candidates.FirstOrDefault(ci => ci.Value == value);
                    if( cellInput != null )
                    {
                        c.Candidates.Remove(cellInput);
                    }
                }
            }
        }

        public void ToggleCandidate(int x, int y, int value)
        {
            var cell = _cells[x, y];

            var existingCandidate = cell.Candidates.FirstOrDefault(candidate => candidate.Value == value);
            if ( existingCandidate == null)
            {
                cell.Candidates.Add(new CellInput
                {
                    Value = value,
                    IsLegal = IsLegalValue(x, y, value)
                });
            } 
            else
            {
                cell.Candidates.Remove(existingCandidate);
            }
        }

        public int GetValue(int x, int y)
        {
            return _cells[x, y].Input.Value;
        }

        private IEnumerable<Cell> CalculateCellsWhichCanSee(int x, int y)
        {
            var indexes = GetIndexesFromCol(x)
                .Concat(GetIndexesFromRow(y))
                .Concat(GetIndexesFromBlock(x, y));

            return indexes.Select(index => _cells[index.x, index.y]).ToArray();
        }

        private IEnumerable<Cell> GetCellsWhichCanSee(int x, int y)
        {
            if (_cellsWhichCanSee[x, y] == null)
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

            return IsLegalValueForCol(x, y, value)
                && IsLegalValueForRow(x, y, value)
                && IsLegalForBlock(x, y, value);
        }

        private bool IsLegalValueFor(int x, int y, int value, IEnumerable<(int x, int y)> indexesToCheck)
        {
            return value == 0 || indexesToCheck
                .Where(index => !(index.x == x && index.y == y))
                .All(index => _cells[index.x, index.y].Input.Value != value);
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
                    sb.Append(_cells[x, y].Input.Value);
                }
            }

            return sb.ToString();
        }

        public object Clone()
        {
            var clone = new Grid();
            Grid.AssignFrom(this, clone);
            return clone;
        }

        private static void AssignFrom(Grid source, Grid destination)
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    var from = source._cells[x, y];
                    var to = destination._cells[x, y];

                    to.Input.Value = from.Input.Value;
                    to.Input.IsLegal = from.Input.IsLegal;

                    to.Candidates.Clear();
                    foreach( var candidate in from.Candidates )
                    {
                        to.Candidates.Add((CellInput) candidate.Clone());
                    }
                }
            }
        }

        private void AssignFrom(Grid source)
        {
            Grid.AssignFrom(source, this);
        }


        private readonly Stack<Grid> _previousStates = new Stack<Grid>();
        private readonly Stack<Grid> _nextStates = new Stack<Grid>();

        public void RecordState()
        {
            _previousStates.Push((Grid) this.Clone());
            _nextStates.Clear();
        }

        public void Undo()
        {
            if (CanUndo)
            {
                var current = (Grid)this.Clone();
                _nextStates.Push(current);

                var previous = _previousStates.Pop();
                this.AssignFrom(previous);
            }
        }

        public void Redo()
        {
            if ( CanRedo )
            {
                var current = (Grid) this.Clone();
                _previousStates.Push(current);

                var next = _nextStates.Pop();
                this.AssignFrom(next);
            }
        }

        public bool CanUndo { get => _previousStates.Count > 0; }
        public bool CanRedo { get => _nextStates.Count > 0; }
    }
}
