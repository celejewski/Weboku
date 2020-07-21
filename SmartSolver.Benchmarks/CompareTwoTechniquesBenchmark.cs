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
    public class CompareTwoTechniquesBenchmark<T, S> where T : ITechniqueFinder where S : ITechniqueFinder
    {
        private readonly ITechniqueFinder _t;
        private readonly ITechniqueFinder _s;
        private readonly ISolvingTechniquesFactory _factory;
        public CompareTwoTechniquesBenchmark()
        {
            _factory = new SolvingTechniqueFactory();
            _t = (ITechniqueFinder) Activator.CreateInstance(typeof(T), _factory);
            _s = (ITechniqueFinder) Activator.CreateInstance(typeof(S), _factory);
        }

        protected IList<IGrid> _grids = TestData.GridsWithCandidates;
        public IList<IGrid> Grids => _grids;

        [Benchmark]
        [ArgumentsSource(nameof(Grids))]
        public IList<ISolvingTechnique> FindAllT(IGrid grid)
        {
            return _t.FindAll(grid).ToList();
        }

        [Benchmark]
        [ArgumentsSource(nameof(Grids))]
        public IList<ISolvingTechnique> FindAllS(IGrid grid)
        {
            return _s.FindAll(grid).ToList();
        }
    }
}
