using Core.Data;
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
        private readonly CandidatesMarkProvider _candidatesMarkProvider;
        private readonly IGridProvider _gridProvider;

        public BaseSingleTechnique(int x, int y, int value, CandidatesMarkProvider candidatesMarkProvider, IGridProvider gridProvider)
        {
            _x = x;
            _y = y;
            _value = value;
            _candidatesMarkProvider = candidatesMarkProvider;
            _gridProvider = gridProvider;
        }

        public abstract string Name { get; }

        public abstract string Desc { get; }

        public virtual void Display()
        {
            _candidatesMarkProvider.SetColor(_x, _y, _value, Enums.Color.Legal);
        }

        public void Execute()
        {
            _gridProvider.SetValue(_x, _y, (InputValue) _value);
        }

        public bool CanExecute()
        {
            return _gridProvider.HasCandidate(_x, _y, (InputValue) _value);
        }
    }
}
