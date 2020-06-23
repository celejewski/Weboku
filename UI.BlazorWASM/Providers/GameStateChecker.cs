using Core.Data;
using System;

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
            for( int y = 0; y < 9; y++ )
            {
                for( int x = 0; x < 9; x++ )
                {
                    if( _gridProvider.GetValue(x, y) == InputValue.Empty || !_gridProvider.IsValueLegal(x, y) )
                    {
                        return;
                    }
                }
            }

            OnSolved?.Invoke();
        }
    }
}
