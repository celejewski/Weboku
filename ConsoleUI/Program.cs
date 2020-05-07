using Core.Data;
using Core.Generator;
using Core.Solver;
using System;
using System.Diagnostics;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var solver = new SimpleSolver();
            var reverseSolver = new SimpleReverseSolver();
            
            var sudokus = new Grid[] {
                    new Grid("000000000000000000000000000000000000000000000000000000000000000000000000000000000"),
                    new Grid("000100047090805600100002900000000206070908050501000000004200008008509060620004000"),
                    new Grid("000050020000007305070390480410900000600000001000008054058063040901500000040020000"),
                    new Grid("001700300002060000780000002209506030000000000040208906500000061000010700006009400")
            };

            var stopwatch = Stopwatch.StartNew();
            for( int i = 0; i < 1; i++ )
            {
                foreach( var sudoku in sudokus )
                {
                    solver.Solve(sudoku);
                    reverseSolver.Solve(sudoku);
                }
            }
            stopwatch.Stop();
            Console.WriteLine("Elapsed time " + stopwatch.ElapsedMilliseconds.ToString());

            var generator = new RandomGenerator();

            stopwatch = Stopwatch.StartNew();
            stopwatch.Stop();
            Console.WriteLine("Generator done in " + stopwatch.ElapsedMilliseconds.ToString());
        }
    }
}
