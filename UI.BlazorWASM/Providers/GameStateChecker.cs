using Core.Data;
using System;
using System.Linq;

namespace UI.BlazorWASM.Providers
{
    public class GameStateChecker
    {
        private readonly IGridProvider _gridProvider;

        public event Action OnSolved;

        public GameStateChecker(IGridProvider gridProvider)
        {
            _gridProvider = gridProvider;
            _gridProvider.OnValueChanged += Check;
        }

        private void Check()
        {
            if( Position.All.All(pos => _gridProvider.HasValue(pos) && _gridProvider.IsValueLegal(pos)) )
            {
                OnSolved?.Invoke();
            }
        }
    }
}
