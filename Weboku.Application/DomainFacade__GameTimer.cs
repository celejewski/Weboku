using System;
using Weboku.Application.Managers;

namespace Weboku.Application
{
    public sealed partial class DomainFacade
    {
        private readonly GameTimerManager _gameTimerManager;

        public TimeSpan Elapsed => _gameTimerManager.Elapsed;

        public event Action OnTimerChanged
        {
            add => _gameTimerManager.OnTimerChanged += value;
            remove => _gameTimerManager.OnTimerChanged -= value;
        }
    }
}