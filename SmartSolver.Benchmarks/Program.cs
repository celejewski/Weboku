using BenchmarkDotNet.Running;

namespace SmartSolver.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var simple = BenchmarkRunner.Run<All>();
        }
    }
}
