using System;
using System.Timers;
using Weboku.Application;

namespace Weboku.UserInterface.Providers
{
    public class PreserveStateProvider
    {
        private bool _isDirty;
        private readonly DomainFacade _domainFacade;
        private readonly Timer _timer;

        public PreserveStateProvider(DomainFacade gridProvider)
        {
            _domainFacade = gridProvider;

            _domainFacade.OnGridChanged += () => _isDirty = true;

            _timer = new Timer();
            _timer.Elapsed += (o, e) => Save();
        }

        public void Save()
        {
            if (_isDirty)
            {
                _domainFacade.Save();
            }
        }

        public void Load()
        {
            try
            {
                _domainFacade.Load();
            }
            catch (Exception e)
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