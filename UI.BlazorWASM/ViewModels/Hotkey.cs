using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.BlazorWASM.ViewModels
{
    public class Hotkey
    {
        public string Key { get; set; }

        public bool Ctrl { get; set; } = false;
        public ICommand Command { get; set; }
        
    }
}
