using System;

namespace Weboku.UserInterface.Providers
{
    public interface IProvider
    {
        event Action OnHintsChanged;
    }
}