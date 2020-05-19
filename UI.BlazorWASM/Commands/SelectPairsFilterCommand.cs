using System.Threading.Tasks;
using UI.BlazorWASM.Filters;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class SelectPairsFilterCommand : ICommand
    {
        private readonly IFilterProvider _filterProvider;

        public SelectPairsFilterCommand(IFilterProvider filterProvider)
        {
            _filterProvider = filterProvider;
        }

        public Task Execute()
        {
            _filterProvider.SetFilter(new PairFilter());
            return Task.CompletedTask;
        }
    }
}
