using Core.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Core.Generator
{
    public class PredefinedGenerator : ISudokuGenerator
    {
        private static Dictionary<string, List<SudokuV1>> _dict;
        private static readonly Random _random = new Random();
        public PredefinedGenerator()
        {
            var file = File.ReadAllText("combined.txt");
            _dict = JsonSerializer.Deserialize<Dictionary<string, List<SudokuV1>>>(file);
        }

        public SudokuV1 Generate(string difficulty)
        {
            if( _dict.ContainsKey(difficulty) )
            {
                var list = _dict[difficulty];
                var index = _random.Next() % list.Count;
                return list[index];
            }
            else
            {
                return new SudokuV1();
            }
        }
    }
}
