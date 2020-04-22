using Core.Data;
using Core.Solver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Generator
{
    public class RandomGenerator : ISudokuGenerator
    {
        private static Random _random = new Random();
        public Grid Generate()
        {
            Grid grid = null;
            int tries = 0;
            do
            {
                tries += 1;
                if (tries % 100 == 0)
                {
                    Console.WriteLine(tries);
                }

                grid = TryGenerate();
            } while( grid == null );

            return grid;
        }
        public Grid TryGenerate()
        {

            var grid = new Grid();
            int tries = _random.Next(30, 200);

            for( int i = 0; i < tries; i++ )
            {
                var (x, y, value) = (
                    _random.Next(0, 9),
                    _random.Next(0, 9),
                    _random.Next(1, 10).ToString()
                    );

                if( grid.IsLegalValue(x, y, value) )
                {
                    grid.SetValue(x, y, value);
                }
            }

            var simpleSolver = new SimpleSolver();
            var simpleReverseSolver = new SimpleReverseSolver();

            try
            {
                if( simpleSolver.Solve(grid) == simpleReverseSolver.Solve(grid))
                {
                    return grid;
                }
            }
            catch
            {

            }

            return null;
        }
    }
}
