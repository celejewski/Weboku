using System.Windows.Input;
using UI.BlazorWASM.ViewModels;

namespace UI.BlazorWASM.Providers
{
    public interface IHotkeyProvider : IProvider
    {
        void Register(Hotkey hotkey);
    }
}
