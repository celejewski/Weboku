using Core.Data;
using SmartSolver.TechniqueFinders;
using System.Collections.Generic;

namespace SmartSolver.Benchmarks
{
    public class CompareTwoTechniquesWithoutCandidatesBenchmark<T, S> 
        : CompareTwoTechniquesBenchmark<T, S> 
        where T : ITechniqueFinder where S : ITechniqueFinder
    {
        public CompareTwoTechniquesWithoutCandidatesBenchmark()
        {
            _grids = TestData.GridsWithoutCandidates;
        }
    }
}
