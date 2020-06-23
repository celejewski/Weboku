using System;

namespace UI.BlazorWASM.Providers
{
    public interface IProvider
    {
        event Action OnChanged;
    }
}
