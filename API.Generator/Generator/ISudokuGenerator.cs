using Core.Data;

namespace API.Generator.Generator
{
    public interface ISudokuGenerator
    {
        Sudoku Generate(string difficulty);
    }
}