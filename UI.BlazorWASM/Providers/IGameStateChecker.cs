using System;

namespace UI.BlazorWASM.Providers
{
    public interface IGameStateChecker
    {
        event Action OnSolved;
    }
}
