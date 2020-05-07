using Core.Data;

namespace Core.Generator
{
    interface ISudokuGenerator
    {
        Sudoku Generate(string difficulty);
    }
}
