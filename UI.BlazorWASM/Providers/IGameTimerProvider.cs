using System;

namespace UI.BlazorWASM.Providers
{
    public interface IGameTimerProvider : IProvider
    {
        TimeSpan Elapsed { get; }
        void Start();
        void Stop();
        
    }
}
