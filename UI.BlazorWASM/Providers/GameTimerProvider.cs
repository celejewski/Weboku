using System;
using System.Timers;

namespace UI.BlazorWASM.Providers
{
    public class GameTimerProvider
    {
        private readonly Timer _timer;
        private readonly ModalProvider _modalProvider;

        public GameTimerProvider(ModalProvider modalProvider)
        {
            _modalProvider = modalProvider;
            _timer = MakeTimer();
            Start();
        }

        private Timer MakeTimer()
        {
            var timer = new Timer(1000);
            timer.Elapsed += (o, e) =>
            {
                bool isPaused = _modalProvider.CurrentState != Application.Enums.ModalState.None;
                if( !isPaused )
                {
                    Elapsed += TimeSpan.FromSeconds(1);
                    OnChanged?.Invoke();
                }
            };
            return timer;
        }

        public TimeSpan Elapsed { get; private set; }

        public event Action OnChanged;

        public void Start()
        {
            _timer.Stop();
            Elapsed = TimeSpan.Zero;
            OnChanged?.Invoke();
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
