using Core.Data;

namespace Core.Generator
{
    public interface ISudokuGenerator
    {
        Sudoku Generate(string difficulty);
    }
}
