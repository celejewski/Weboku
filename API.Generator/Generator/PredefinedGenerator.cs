using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Weboku.Core.Data;

namespace Weboku.Generator.Api.Generator
{
    public class PredefinedGenerator : ISudokuGenerator
    {
        private static Dictionary<string, List<Sudoku>> _dict;
        private static readonly Random _random = new Random();

        public PredefinedGenerator()
        {
            var file = File.ReadAllText("combinedv2.txt");
            _dict = JsonSerializer.Deserialize<Dictionary<string, List<Sudoku>>>(file);
        }

        public Sudoku Generate(string difficulty)
        {
            if (_dict.ContainsKey(difficulty))
            {
                var list = _dict[difficulty];
                var index = _random.Next() % list.Count;
                var sudoku = list[index];
                sudoku.Steps = Enumerable.Empty<string>();
                return sudoku;
            }
            else
            {
                return new Sudoku();
            }
        }
    }
}