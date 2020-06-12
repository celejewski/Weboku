using Core.Data;
using System;

namespace UI.BlazorWASM.Providers
{
    public class GridProvider : IGridProvider
    {
        private readonly ISudokuProvider _sudokuProvider;

        public GridProvider(ISudokuProvider sudokuProvider)
        {
            _sudokuProvider = sudokuProvider;
        }

        public InputValue GetValue(int x, int y) => (InputValue) _sudokuProvider.Cells[x, y].Input.Value;
        public void SetValue(int x, int y, InputValue value)
        {
            _sudokuProvider.SetValue(x, y, (int) value);
        }

        public bool HasCandidate(int x, int y, InputValue value)
        {
            return _sudokuProvider.Cells[x, y].Candidates.ContainsKey((int) value);
        }

        public void AddCandidate(int x, int y, InputValue value)
        {
            if( !HasCandidate(x, y, value) )
            {
                _sudokuProvider.ToggleCandidate(x, y, (int) value);
            }
        }

        public void RemoveCandidate(int x, int y, InputValue value)
        {
            if( HasCandidate(x, y, value) )
            {
                _sudokuProvider.ToggleCandidate(x, y, (int) value);
            }
        }

        public void ClearCandidates(int x, int y)
        {
            for( int value = 1; value < 10; value++ )
            {
                RemoveCandidate(x, y, (InputValue) value);
            }
        }

        public void ToggleCandidate(int x, int y, InputValue value)
        {
            _sudokuProvider.ToggleCandidate(x, y, (int) value);
        }

        public void FillCandidates()
        {
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    for( int value = 1; value < 10; value++ )
                    {
                        AddCandidate(x, y, (InputValue) value);
                    }
                }
            }
        }

        public void ClearCandidates()
        {

            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    for( int value = 1; value < 10; value++ )
                    {
                        RemoveCandidate(x, y, (InputValue) value);
                    }
                }
            }
        }

        public bool IsGiven(int x, int y)
        {
            return _sudokuProvider.Cells[x, y].IsGiven;
        }

        public bool IsValueLegal(int x, int y)
        {
            return _sudokuProvider.Cells[x, y].Input.IsLegal;
        }

        public bool IsCandidateLegal(int x, int y, InputValue value)
        {
            return _sudokuProvider.Cells[x, y].Candidates[(int)value].IsLegal;
        }

        public int GetCandidatesCount(int x, int y)
        {
            return _sudokuProvider.Cells[x, y].Candidates.Count;
        }
    }
}
