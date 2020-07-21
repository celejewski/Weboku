using SmartSolver.Benchmarks;
using SmartSolver.SolvingTechniques;
using SmartSolver.TechniqueFinders;
using System;
using System.Linq;

namespace SmartSolver.Profiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new SolvingTechniqueFactory();
            var solver = new Solver(factory);

            for( int i = 0; i < 100; i++ )
            {
                foreach( var grid in TestData.GridsWithCandidates )
                {
                    var steps = solver.AllSteps(grid).ToList();
                }
            }
        }
    }
}
