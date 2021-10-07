using System;
using System.Timers;
using Weboku.Application.Data;

namespace Weboku.Application
{
    public sealed partial class DomainFacade
    {
        private bool _isDirty;
        private Timer _timer;


        public void Save()
        {
            if (_isDirty)
            {
                _storageManager.Save(new StorageDto(_grid, Difficulty));
            }
        }

        public void Load()
        {
            try
            {
                var storageDto = _storageManager.Load();
                StartNewGame(storageDto.Grid, storageDto.Difficulty);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void StartAutoSave(TimeSpan timeSpan)
        {
            _timer = new Timer();
            _timer.Elapsed += (o, e) => Save();
            _timer.Interval = timeSpan.TotalMilliseconds;
            _timer.Start();
        }
    }
}