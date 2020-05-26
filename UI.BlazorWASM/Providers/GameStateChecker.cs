using System;

namespace UI.BlazorWASM.Providers
{
    public class GameStateChecker : IGameStateChecker
    {
        private readonly ISudokuProvider _sudokuProvider;

        public event Action OnSolved;

        public GameStateChecker(ISudokuProvider sudokuProvider)
        {
            _sudokuProvider = sudokuProvider;
            _sudokuProvider.OnValueOrCandidatesChanged += Check;
        }

        private void Check()
        {
            for( int y = 0; y < 9; y++ )
            {
                for( int x = 0; x < 9; x++ )
                {
                    var cell = _sudokuProvider.Cells[x, y];
                    if (cell.Input.Value == 0 || !cell.Input.IsLegal)
                    {
                        return;
                    }
                }
            }

            OnSolved?.Invoke();
        }
    }
}
