using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using UI.BlazorWASM.Enums;
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

        public HotkeyProvider(CommandProvider commandProvider, NumpadMenuBuilder numpadMenuBuilder)
        {
            Register(new Hotkey { Command = commandProvider.FindAllCandidates(), Key = "f", Ctrl = true });
            for( int value = 1; value < 10; value++ )
            {
                Register(new Hotkey { Command = numpadMenuBuilder.SelectValue(value), Key = value.ToString() });
            }
            Register(new Hotkey { Command = numpadMenuBuilder.Redo(), Key = "y", Ctrl = true });
            Register(new Hotkey { Command = numpadMenuBuilder.Undo(), Key = "z", Ctrl = true });
            Register(new Hotkey { Command = numpadMenuBuilder.Pairs(), Key = "x" });
            Register(new Hotkey { Command = numpadMenuBuilder.ClearColors(), Key = "r" });
            Register(new Hotkey { Command = numpadMenuBuilder.SelectCleanerAction(), Key = "0" });

            
            var dict = new Dictionary<CellColor, string>
            {
                { CellColor.First, "a" },
                { CellColor.Second, "s" },
                { CellColor.Third, "d" },
                { CellColor.Fourth, "f" }
            };

            foreach( var item in dict )
            {
                Register(new Hotkey { Command = numpadMenuBuilder.SelectColor(item.Key), Key = item.Value });
            }
        }
    }
}
