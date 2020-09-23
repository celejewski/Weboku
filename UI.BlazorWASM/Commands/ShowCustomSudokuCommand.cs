using System.Threading.Tasks;

namespace UI.BlazorWASM.Commands
{
    public class ShowCustomSudokuCommand : ICommand
    {
        public Task Execute()
        {
            return Task.CompletedTask;
        }
    }
}
