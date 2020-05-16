using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.BlazorWASM.ViewModels;

namespace UI.BlazorWASM.Providers
{
    public class HotkeyProvider : IHotkeyProvider
    {
        public event Action OnChanged;
        public IList<Hotkey> Hotkeys { get; } = new List<Hotkey>();

        public void Register(Hotkey hotkey)
        {
            Hotkeys.Add(hotkey);
            OnChanged?.Invoke();
        }
    }
}
