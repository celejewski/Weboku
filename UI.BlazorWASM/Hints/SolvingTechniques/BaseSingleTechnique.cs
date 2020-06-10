using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Hints.SolvingTechniques
{
    public abstract class BaseSingleTechnique : ISolvingTechnique
    {
        protected readonly int _x;
        protected readonly int _y;
        protected readonly int _value;

        public BaseSingleTechnique(int x, int y, int value)
        {
            _x = x;
            _y = y;
            _value = value;
        }

        public abstract string Name { get; }

        public abstract string Desc { get; }

        public virtual void Display(HintsProvider hintsProvider)
        {
            hintsProvider.CandidatesMarkProvider.SetColor(_x, _y, _value, Enums.Color.Legal);
        }

        public void Execute(ISudokuProvider sudokuProvider)
        {
            sudokuProvider.SetValue(_x, _y, _value);
        }

        public bool CanExecute(ISudokuProvider sudokuProvider)
        {
            return sudokuProvider.Cells[_x, _y].Candidates.ContainsKey(_value);
        }
    }
}
