using Application;
using Core.Data;
using Core.Serializers;
using System;
using System.Timers;
using UI.BlazorWASM.Enums;

namespace UI.BlazorWASM.Providers
{
    public class PreserveStateProvider
    {
        private bool _isDirty = false;
        private readonly DomainFacade _domainFacade;
        private readonly StorageProvider _storageProvider;
        private readonly Timer _timer;
        private readonly IGridSerializer _serializer = GridSerializerFactory.Make(GridSerializerName.Base64);
        public PreserveStateProvider(DomainFacade gridProvider, StorageProvider storageProvider)
        {
            _domainFacade = gridProvider;
            _storageProvider = storageProvider;

            _domainFacade.OnValueOrCandidateChanged += () => _isDirty = true;

            _timer = new Timer();
            _timer.Elapsed += (o, e) => Save();
        }

        public void PauseAutoSave()
        {
            _timer.Stop();
        }

        public void ResumeAutoSave()
        {
            _timer.Start();
        }

        public void Save()
        {
            if( _isDirty )
            {
                _storageProvider.Save(StorageKey.Grid, _serializer.Serialize(_domainFacade.Grid));
                _storageProvider.Save(StorageKey.Difficulty, _domainFacade.Difficulty);
                _isDirty = false;
            }
        }

        public void Load()
        {
            try
            {
                if( _storageProvider.HasSaved(StorageKey.Grid) )
                {
                    _domainFacade.Grid = _serializer.Deserialize(_storageProvider.Load<string>(StorageKey.Grid));
                }
                if( _storageProvider.HasSaved(StorageKey.Difficulty) )
                {
                    _domainFacade.Difficulty = _storageProvider.Load<Difficulty>(StorageKey.Difficulty);
                }
            }
            catch( Exception e )
            {
                Console.WriteLine(e);
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
