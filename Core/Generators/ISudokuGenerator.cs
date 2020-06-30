using Core.Data;
using System.Threading.Tasks;

namespace Core.Generators
{
    public interface ISudokuGenerator
    {
        Task<SudokuV1> Generate(string difficulty);
    }
}
