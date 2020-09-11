﻿using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Collections.Generic;

namespace UI.BlazorWASM.Providers
{
    public class HotkeyProvider
    {
        private static IList<Hotkey> Hotkeys { get; } = new List<Hotkey>();

        public void Register(Hotkey hotkey)
        {
            Hotkeys.Add(hotkey);
        }

        [JSInvokable]
        public static void OnKeyDown(KeyboardEventArgs e)
        {
            foreach( var item in Hotkeys )
            {
                if( item.Key == e.Key
                    && item.Ctrl == e.CtrlKey )
                {
                    _ = item.Command.Execute();
                }
            }
        }
    }
}
