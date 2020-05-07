using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Data;
using Core.Generator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
