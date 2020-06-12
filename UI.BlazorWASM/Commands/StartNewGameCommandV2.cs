using Core.Generators;
using System.Threading.Tasks;
using UI.BlazorWASM.Component.Modals;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class StartNewGameCommandV2 : ICommand
    {
        private readonly RESTGridGeneratorV2 _generator;
        private readonly IGridProvider _gridProvider;
        private readonly ModalProvider _modalProvider;

        public StartNewGameCommandV2(RESTGridGeneratorV2 generator, IGridProvider gridProvider, ModalProvider modalProvider)
        {
            _generator = generator;
            _gridProvider = gridProvider;
            _modalProvider = modalProvider;
        }

        public async Task Execute()
        {
            var sudoku = _generator.WithGiven("Medium");
            _gridProvider.Grid = await sudoku;
            _modalProvider.SetModalState(ModalState.None);

        }
    }
}
