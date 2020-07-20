using Core.Converters;
using Core.Data;
using Core.Generators;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolver.Benchmarks
{
    public static class TestData
    {
        public static IList<IGrid> GridsWithoutCandidates { get; }
        public static IList<IGrid> GridsWithCandidates { get; }
        static TestData()
        {

            var converter = new HodokuGridConverter(new EmptyGridGenerator());
            var givens = new string[]
                {
                ".....58...4.........7428.6.71..64..5....1....9..38..76.2.1536.........4...52.....",
                "............8.1247..426.1...619.....9.7...5.2.....596...2.978..7356.2............",
                "52...8....6..235....15.......5.1..797...8...534..9.1.......28....285..9....4...23",
                "..487..5......3..639......8......53...39526...69......8......459..7......2..381..",
                "62...47......1.....456...2.8.....4...6.9.3.5...1.....7.7...184.....9......65...71",
            };
            GridsWithoutCandidates = givens.Select(text => converter.FromText(text)).ToArray();
            GridsWithCandidates = GridsWithoutCandidates.Select(grid => grid.Clone()).ToArray();
            foreach( var grid in GridsWithCandidates )
            {
                grid.FillCandidates();
            }
        }

        
    }
}
