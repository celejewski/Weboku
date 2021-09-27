using System;
using System.Linq;
using Application;
using Core.Data;

namespace Weboku.UserInterface.Providers
{
    public class GameStateChecker
    {
        private readonly DomainFacade _domainFacade;

        public event Action OnSolved;

        public GameStateChecker(DomainFacade domainFacade)
        {
            _domainFacade = domainFacade;
            _domainFacade.OnGridChanged += RaiseEventIfSudokuIsSolved;
        }

        private void RaiseEventIfSudokuIsSolved()
        {
            if (Position.Positions.All(position => _domainFacade.HasValue(position) && _domainFacade.IsValueLegal(position)))
            {
                OnSolved?.Invoke();
            }
        }
    }
}