using System.Threading.Tasks;
using UI.BlazorWASM.Filters;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class SelectPairsFilterCommand : ICommand
    {
        private readonly FilterProvider _filterProvider;

        public SelectPairsFilterCommand(FilterProvider filterProvider)
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
