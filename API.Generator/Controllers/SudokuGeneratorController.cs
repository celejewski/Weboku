using API.Generator.Generator;
using Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Generator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SudokuGeneratorController : ControllerBase
    {
        private readonly ISudokuGenerator _sudokuGenerator;

        public SudokuGeneratorController(ISudokuGenerator sudokuGenerator)
        {
            _sudokuGenerator = sudokuGenerator;
        }

        [HttpGet("{difficulty?}")]
        public Sudoku Get(string difficulty = "Medium")
        {
            return _sudokuGenerator.Generate(difficulty);
        }
    }
}