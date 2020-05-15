using UI.BlazorWASM.ViewModels;

namespace UI.BlazorWASM.Providers
{
    public class NumpadMenuItemBuilderProvider
    {
        private readonly IFilterProvider _filterProvider;

        public NumpadMenuItemBuilderProvider(IFilterProvider filterProvider)
        {
            _filterProvider = filterProvider;
        }

        public NumpadMenuItemBuilder New()
        {
            return new NumpadMenuItemBuilder(_filterProvider);
        }
    }
}
