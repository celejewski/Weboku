using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Filters;
using UI.BlazorWASM.ViewModels;

namespace UI.BlazorWASM.Services
{
    public class AppData
    {
        public AppData()
        {
            Filter = new SelectedValueFilter(1);
        }

        public IFilter Filter { get; set; }
        public IClickableAction ClickableAction { get; set; }
        public StandardAction StandardAction { get; set; }
        public NumpadMenuItem[] MenuItems { get; private set; }
    }
}
