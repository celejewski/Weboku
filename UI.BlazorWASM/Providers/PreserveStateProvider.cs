using Core;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace UI.BlazorWASM.Providers
{
    public class PreserveStateProvider
    {
        private bool _isDirty = false;
        private readonly DomainFacade _gridProvider;
        private readonly StorageProvider _storageProvider;
        private readonly Timer _timer;
        public PreserveStateProvider(DomainFacade gridProvider, StorageProvider storageProvider)
        {
            _gridProvider = gridProvider;
            _storageProvider = storageProvider;

            _gridProvider.OnValueOrCandidateChanged += () => _isDirty = true;

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
                _isDirty = false;
            }
        }

        public async Task Load()
        {
            if( await _storageProvider.HasSavedGrid() )
            {
                _gridProvider.Grid = await _storageProvider.LoadGrid();
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
