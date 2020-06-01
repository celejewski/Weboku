using System;
using System.Timers;

namespace UI.BlazorWASM.Providers
{
    public class GameTimerProvider : IGameTimerProvider
    {
        private readonly Timer _timer;
        private readonly ModalProvider _modalProvider;

        public GameTimerProvider(ModalProvider modalProvider)
        {
            _timer = new Timer(1000);
            _timer.Elapsed += (o, e) =>
            {
                bool isPaused = _modalProvider.Modal != Component.Modals.ModalState.None;
                if( !isPaused )
                {
                    Elapsed += TimeSpan.FromSeconds(1);
                }
                OnChanged?.Invoke();
            };
            _modalProvider = modalProvider;
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
