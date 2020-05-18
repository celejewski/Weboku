using System;
using System.Timers;

namespace UI.BlazorWASM.Providers
{
    public class GameTimerProvider : IGameTimerProvider
    {
        private DateTime _startTime;
        private readonly Timer _timer;
        private DateTime _lastUpdate;

        public GameTimerProvider()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += (o, e) =>
            {
                _lastUpdate = DateTime.Now;
                OnChanged?.Invoke();
            };
        }

        public TimeSpan Elapsed => _lastUpdate - _startTime;

        public event Action OnChanged;

        public void Start()
        {
            _timer.Stop();
            _startTime = DateTime.Now;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
