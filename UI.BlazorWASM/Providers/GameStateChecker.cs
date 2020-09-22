using Core;
using Core.Data;
using System;
using System.Linq;

namespace UI.BlazorWASM.Providers
{
    public class GameStateChecker
    {
        private readonly DomainFacade _domainFacade;

        public event Action OnSolved;

        public GameStateChecker(DomainFacade domainFacade)
        {
            _domainFacade = domainFacade;
            _domainFacade.OnValueChanged += RaiseEventIfSudokuIsSolved;
        }

        private void RaiseEventIfSudokuIsSolved()
        {
            if( Position.Positions.All(position => _domainFacade.HasValue(position) && _domainFacade.IsValueLegal(position)) )
            {
                OnSolved?.Invoke();
            }
        }
    }
}
