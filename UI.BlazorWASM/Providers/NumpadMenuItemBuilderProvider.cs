using UI.BlazorWASM.ViewModels;

namespace UI.BlazorWASM.Providers
{
    public class NumpadMenuItemBuilderProvider
    {
        private readonly IFilterProvider _filterProvider;
        private readonly IClickableActionProvider _clickableActionProvider;

        public NumpadMenuItemBuilderProvider(IFilterProvider filterProvider, IClickableActionProvider clickableActionProvider)
        {
            _filterProvider = filterProvider;
            _clickableActionProvider = clickableActionProvider;
        }

        public NumpadMenuItemBuilder New()
        {
            return new NumpadMenuItemBuilder(_filterProvider, _clickableActionProvider);
        }
    }
}
