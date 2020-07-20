using BenchmarkDotNet.Attributes;
using Core.Data;
using SmartSolver.SolvingTechniques;
using SmartSolver.TechniqueFinders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolver.Benchmarks
{
    [MemoryDiagnoser]
    public class SolvingTechniqueBenchmark<T> where T : ITechniqueFinder
    {

        private readonly ITechniqueFinder _techniqueFinder;
        private readonly ISolvingTechniquesFactory _factory;
        public SolvingTechniqueBenchmark()
        {
            _factory = new SolvingTechniqueFactory();
            _techniqueFinder = (ITechniqueFinder) Activator.CreateInstance(typeof(T), _factory);
        }

        public IList<IGrid> Grids => TestData.GridsWithoutCandidates;
        [Benchmark]
        [ArgumentsSource(nameof(Grids))]
        public IList<ISolvingTechnique> FindAll(IGrid grid)
        {
            return _techniqueFinder.FindAll(grid).ToList();

        }
    }
}
