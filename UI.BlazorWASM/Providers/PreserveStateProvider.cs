using System;
using System.Threading.Tasks;
using System.Timers;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Providers
{
    public class PreserveStateProvider
    {
        private bool _isDirty = false;
        private readonly SudokuProvider _sudokuProvider;
        private readonly IGridProvider _gridProvider;
        private readonly StorageProvider _storageProvider;
        private readonly Timer _timer;
        public PreserveStateProvider(SudokuProvider sudokuProvider, IGridProvider gridProvider, StorageProvider storageProvider)
        {
            _sudokuProvider = sudokuProvider;
            _gridProvider = gridProvider;
            _storageProvider = storageProvider;

            _sudokuProvider.OnChanged += () => _isDirty = true;
            _gridProvider.OnValueOrCandidatesChanged += () => _isDirty = true;

            _timer = new Timer();
            _timer.Elapsed += (o, e) => _ = Save();
        }

        public void PauseAutoSave()
        {
            _timer.Stop();
        }

        public void ResumeAutoSave()
        {
            _timer.Start();
        }

        public async Task Save()
        {
            if( _isDirty )
            {
                await _storageProvider.SaveGrid(_gridProvider.Grid);
                await _storageProvider.SaveSudoku(_sudokuProvider.Sudoku);
                await _storageProvider.SavePreferredDifficulty(_sudokuProvider.PreferredDifficulty);
                _isDirty = false;
            }
        }

        public async Task Load()
        {
            if( await _storageProvider.HasSavedGrid() )
            {
                _gridProvider.Grid = await _storageProvider.LoadGrid();
            }
            if( await _storageProvider.HasSavedSudoku() )
            {
                _sudokuProvider.Sudoku = await _storageProvider.LoadSudoku();
            }
            if (await _storageProvider.HasSaved(StorageKey.PreferredDifficulty))
            {
                _sudokuProvider.PreferredDifficulty = await _storageProvider.LoadPreferredDifficulty();
            }
        }

        public void AutoSave(TimeSpan timeSpan)
        {
            _timer.Stop();
            _timer.Interval = timeSpan.TotalMilliseconds;
            _timer.Start();
        }
    }
}
