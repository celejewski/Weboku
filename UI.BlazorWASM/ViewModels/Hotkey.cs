using UI.BlazorWASM.Commands;

namespace UI.BlazorWASM.ViewModels
{
    public class Hotkey
    {
        public string Key { get; set; }

        public bool Ctrl { get; set; } = false;
        public ICommand Command { get; set; }

    }
}
