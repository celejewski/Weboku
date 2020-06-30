using Core.Data;

namespace Core.Generator
{
    public interface ISudokuGenerator
    {
        SudokuV1 Generate(string difficulty);
    }
}
