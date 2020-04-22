using Core.Generator;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var generator = new RandomGenerator();
            var grid = generator.Generate();
            Console.WriteLine(grid.ToString());

        }
    }
}
