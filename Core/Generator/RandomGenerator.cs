using Core.Data;
using Core.Solver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Generator
{
    public class RandomGenerator : ISudokuGenerator
    {
        private static readonly Random _random = new Random();
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
            int tries = 70;


            var simpleSolver = new SimpleSolver();
            var simpleReverseSolver = new SimpleReverseSolver();

            for( int i = 0; i < 40; i++ )
            {
                TryAddGiven(grid);
            }

            for( int i = 0; i < tries; i++ )
            {
                var a = simpleSolver.Solve(grid);
                var b = simpleReverseSolver.Solve(grid);
                if( a == b && a != null )
                {
                    return grid;
                }
                TryAddGiven(grid);
            }

            return null;
        }

        public Grid TryAddGiven(Grid grid)
        {
            var (x, y, value) = (
                _random.Next(0, 9),
                _random.Next(0, 9),
                _random.Next(1, 10)
                );

            if( grid.IsLegalValue(x, y, value) )
            {
                grid.SetValue(x, y, value);
            }

            return grid;
        }
    }
}
