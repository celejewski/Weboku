using Core.Data;
using SmartSolver.SolvingTechniques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSolver.TechniqueFinders
{
    public class XWingFinder : BaseTechniqueFinder
    {
        private readonly ISolvingTechniquesFactory _factory;

        public XWingFinder(ISolvingTechniquesFactory factory)
        {
            _factory = factory;
        }

        public override IEnumerable<ISolvingTechnique> FindAll(IGrid grid)
        {

            foreach( var value in InputValue.NonEmpty )
            {
                foreach( var houses in new[] { Position.Rows, Position.Cols } )
                {
                    var filtered = houses.Select(
                        house => house.Where(pos => grid.HasCandidate(pos, value)))
                        .Where(positions => positions.Count() == 2);

                    var housesCombined = new List<(IEnumerable<Position> first, IEnumerable<Position> second)>();
                    for( int i = 0; i < filtered.Count(); i++ )
                    {
                        for( int j = i + 1; j < filtered.Count(); j++ )
                        {
                            var pair = (first: filtered.ElementAt(i), second: filtered.ElementAt(j));
                            housesCombined.Add(pair);
                        }
                    }

                    var housesCombinedAndFiltered = housesCombined.Where(pair => pair.first.All(pos1 => pair.second.Any(pos2 => pos1.x == pos2.x || pos1.y == pos2.y)));
                    if( housesCombinedAndFiltered.Any() )
                    {
                        var firstHouse = housesCombinedAndFiltered.First().first;
                        var secondHouse = housesCombinedAndFiltered.First().second;

                        var positions = firstHouse.Concat(secondHouse);
                        var positionsSeen = positions.SelectMany(pos => Position.Cols[pos.x].Concat(Position.Rows[pos.y]));
                        var positionsToRemove = positionsSeen.Except(positions)
                            .Where(pos => grid.HasCandidate(pos, value));

                        yield return new XWing(value, positions, positionsToRemove, Position.GetHouse(firstHouse));

                    };
                }
            }
        }
    }
}
