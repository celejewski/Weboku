using Core.Data;
using System;
using System.Collections.Generic;

namespace UI.BlazorWASM.Providers
{
    public class SudokuProvider : IProvider
    {
        private Sudoku _sudoku = new Sudoku();
        
        public Sudoku Sudoku 
        { 
            get => _sudoku;
            set
            {
                _sudoku = value;
                OnChanged?.Invoke();
            }
        }

        public string Difficulty => Sudoku.Difficulty;
        public string Givens => Sudoku.Given;
        public int Rating => Sudoku.Rating;

        public string Solution => Sudoku.Solution;
        public InputValue GetSolution(int x, int y) => (InputValue) int.Parse(Solution.Substring(y * 9 + x, 1));
        public IEnumerable<string> Steps => Sudoku.Steps;

        public event Action OnChanged;
    }
}
