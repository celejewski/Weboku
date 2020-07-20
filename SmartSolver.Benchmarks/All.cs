using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using Core.Data;
using SmartSolver.SolvingTechniques;
using SmartSolver.TechniqueFinders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolver.Benchmarks
{
    [MemoryDiagnoser]
    [EventPipeProfiler(EventPipeProfile.CpuSampling)]
    public class All
    {
        public Dictionary<Type, ITechniqueFinder> TechniqueFinders { get; }
        public IList<IGrid> Grids => TestData.GridsWithCandidates;

        public IList<Type> Types { get; }

        public All()
        {
            Types = new[]
            {
                typeof(FullHouseFinder),
                typeof(HiddenSingleWithoutCandidatesFinder),
                typeof(NakedSingleFinder),
                typeof(HiddenSingleFinder),
                typeof(LockedCandidatesPointingFinder),
                typeof(LockedCandidatesClaimingFinder),
                typeof(NakedPairFinder),
                typeof(NakedTripleFinder),
                typeof(HiddenPairFinder),
                typeof(XWingFinder),
                typeof(SkyscrapperFinder),
                typeof(XYWingFinder),
                typeof(NakedQuadrupleFinder),
                typeof(HiddenTripleFinder),
            };

            var factory = new SolvingTechniqueFactory();
            TechniqueFinders = Types.ToDictionary(
                type => type, 
                type => (ITechniqueFinder) Activator.CreateInstance(type, factory)
                );
        }

        public IEnumerable<object[]> Data()
        {
            foreach( var type in Types )
            {

                foreach( var grid in Grids )
                {
                    yield return new object[] { type, grid };
                }
            }
            
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public List<ISolvingTechnique> Bench(Type t, IGrid g)
        {
            var technique = TechniqueFinders[t];
            return technique.FindAll(g).ToList();
        }

    }
}
