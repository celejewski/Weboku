using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
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


        public void OnKeyDown(KeyboardEventArgs e)
        {
            foreach( var item in Hotkeys )
            {
                if( item.Key == e.Key
                    && item.Ctrl == e.CtrlKey )
                {
                    item.Command.Execute();
                }
            }
        }
    }
}
