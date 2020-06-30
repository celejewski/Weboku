using Core.Data;
using Core.Generator;
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
        public SudokuV1 Get(string difficulty = "Medium")
        {
            return _sudokuGenerator.Generate(difficulty);
        }
    }
}
