using System.Threading.Tasks;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class RestartCommand : ICommand
    {
        private readonly ISudokuProvider _sudokuProvider;
        private readonly IGridHistoryManager _gridHistoryManager;

        public RestartCommand(ISudokuProvider sudokuProvider, IGridHistoryManager gridHistoryManager)
        {
            _sudokuProvider = sudokuProvider;
            _gridHistoryManager = gridHistoryManager;
        }
        public Task Execute()
        {
            _gridHistoryManager.Save();
            _sudokuProvider.Restart();
            _sudokuProvider.ClearCandidates();
            return Task.CompletedTask;
        }
    }
}
