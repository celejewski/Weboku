using Core.Data;
using System.Threading.Tasks;

namespace Core.Generators
{
    public interface ISudokuGenerator
    {
        Task<Sudoku> Generate(string difficulty);
    }
}
