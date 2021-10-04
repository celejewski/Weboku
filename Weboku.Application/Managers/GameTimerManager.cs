using System;
using System.Timers;

namespace Weboku.Application.Managers
{
    public class GameTimerManager
    {
        public GameTimerManager()
        {
            _timer = MakeTimer();
        }

        private readonly Timer _timer;

        private Timer MakeTimer()
        {
            var timer = new Timer(1000);
            timer.Elapsed += OnTimerOnElapsed;
            timer.Start();

            _isPaused = true;
            return timer;
        }

        private void OnTimerOnElapsed(object o, ElapsedEventArgs e)
        {
            if (_isPaused) return;

            Elapsed += TimeSpan.FromSeconds(1);
            OnTimerChanged?.Invoke();
        }

        public TimeSpan Elapsed { get; private set; }

        public event Action OnTimerChanged;

        public void StartTimer()
        {
            _timer.Stop();
            Elapsed = TimeSpan.Zero;
            OnTimerChanged?.Invoke();
            _timer.Start();
        }

        private bool _isPaused;

        public void Pause()
        {
            _isPaused = true;
        }

        public void Unpause()
        {
            _isPaused = false;
        }

        public void StopTimer()
        {
            _timer.Stop();
        }
    }
}