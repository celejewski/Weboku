using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using UI.BlazorWASM.Commands;
using UI.BlazorWASM.ViewModels;

namespace UI.BlazorWASM.Providers
{
    public class HotkeyProvider
    {
        public event Action OnChanged;
        private static IList<Hotkey> Hotkeys { get; } = new List<Hotkey>();

        private void Register(Hotkey hotkey)
        {
            Hotkeys.Add(hotkey);
            OnChanged?.Invoke();
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

        public HotkeyProvider(FindAllCandidatesCommand findAllCandidatesCommand, NumpadMenuBuilder numpadMenuBuilder)
        {
            Register(new Hotkey { Command = findAllCandidatesCommand, Key = "f", Ctrl = true });
            for( int value = 1; value < 10; value++ )
            {
                Register(new Hotkey { Command = numpadMenuBuilder.SelectValue(value), Key = value.ToString() });
            }
            Register(new Hotkey { Command = numpadMenuBuilder.Redo(), Key = "y", Ctrl = true });
            Register(new Hotkey { Command = numpadMenuBuilder.Undo(), Key = "z", Ctrl = true });
            Register(new Hotkey { Command = numpadMenuBuilder.Pairs(), Key = "x" });
            Register(new Hotkey { Command = numpadMenuBuilder.ClearColors(), Key = "r" });
        }

        private class Hotkey
        {
            public string Key { get; set; }

            public bool Ctrl { get; set; } = false;
            public ICommand Command { get; set; }

        }
    }
}
