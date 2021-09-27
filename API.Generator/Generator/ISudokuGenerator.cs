using Weboku.Core.Data;

namespace Weboku.Generator.Api.Generator
{
    public interface ISudokuGenerator
    {
        Sudoku Generate(string difficulty);
    }
}