using Microsoft.AspNetCore.Mvc;
using Weboku.Core.Data;
using Weboku.Generator.Api.Generator;

namespace Weboku.Generator.Api.Controllers
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