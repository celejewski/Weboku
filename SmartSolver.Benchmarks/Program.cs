using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Diagnostics.Windows;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Core.Converters;
using Core.Data;
using Core.Generators;
using SmartSolver.SolvingTechniques;
using SmartSolver.TechniqueFinders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SmartSolver.Benchmarks
{
    [EventPipeProfiler(EventPipeProfile.CpuSampling)]
    //[SimpleJob(launchCount: 1, warmupCount: 1, targetCount: 5, invocationCount: 500, id: "QuickJob")]    
    public class HiddenSingleBenchmark
    {
        private readonly ITechniqueFinder _techniqueFinderOld;
        private readonly ITechniqueFinder _techniqueFinderNew;
        private readonly ISolvingTechniquesFactory _factory;

        public IList<IGrid> Grids => TestData.GridsWithoutCandidates;
        public HiddenSingleBenchmark()
        {
            _factory = new SolvingTechniqueFactory();
            _techniqueFinderOld = new HiddenSingleWithoutCandidatesFinder(_factory);
            _techniqueFinderNew = new HiddenSingleWithoutCandidatesFinderNew(_factory);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Grids))]
        public IList<ISolvingTechnique> FindAll(IGrid grid)
        {
            return _techniqueFinderOld.FindAll(grid).ToList();
            
        }

        [Benchmark]
        [ArgumentsSource(nameof(Grids))]
        public IList<ISolvingTechnique> FindAllNew(IGrid grid)
        {
            return _techniqueFinderNew.FindAll(grid).ToList();
        }
    }


    [EventPipeProfiler(EventPipeProfile.CpuSampling)] // <-- Enables new profiler
    public class IntroEventPipeProfiler
    {
        [Benchmark]
        public void Sleep() => Thread.Sleep(2000);
    }

    public class NakedPairBenchmark : SolvingTechniqueBenchmark<NakedPairFinder> { }

    class Program
    {
        static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<HiddenSingleBenchmark>(
            //    DefaultConfig.Instance
            //        .WithSummaryStyle(
            //            SummaryStyle.Default.WithMaxParameterColumnWidth(80)
            //            )
            //    );

            var simple = BenchmarkRunner.Run<All>();
        }
    }
}
