using Core.Data;
using SmartSolver.SolvingTechniques;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolver.TechniqueFinders
{
    public class SkyscrapperFinder : BaseTechniqueFinder
    {
        private readonly ISolvingTechniquesFactory _factory;

        public SkyscrapperFinder(ISolvingTechniquesFactory factory)
        {
            _factory = factory;
        }

        public override IEnumerable<ISolvingTechnique> FindAll(IGrid grid)
        {
            foreach( var value in InputValue.NonEmpty )
            {
                foreach( var houses in new[] { Position.Rows, Position.Cols } )
                {
                    var housesWithCandidate = houses.Select(house => house.Where(pos => grid.HasCandidate(pos, value)));
                    var housesWithTwoCells = housesWithCandidate.Where(house => house.Count() == 2);

                    var housesPaired = housesWithTwoCells.SelectMany(
                        (house1, i) => housesWithTwoCells.Skip(i + 1).Select(house2 => (house1, house2)));

                    var possibleSkyscrappers = housesPaired
                        .Where(houses =>
                        houses.house1.Any(pos1 => houses.house2.Any(pos2 => pos1.x == pos2.x || pos1.y == pos2.y))
                        && houses.house1.Concat(houses.house2).Select(pos => pos.block).Distinct().Count() == 4
                        );

                    foreach( var (house1, house2) in possibleSkyscrappers )
                    {
                        var pos1 = house1.FirstOrDefault(pos1 => house2.All(pos2 => !pos1.IsSharingHouseWith(pos2)));
                        var pos2 = house2.FirstOrDefault(pos2 => house1.All(pos1 => !pos1.IsSharingHouseWith(pos2)));

                        var positionsToRemoveFrom = Position.GetOtherPositionsSeenBy(pos1, pos2)
                            .Where(pos => grid.HasCandidate(pos, value));

                        var base1 = house1.First(pos => !pos.Equals(pos1));
                        var base2 = house2.First(pos => !pos.Equals(pos2));
                        yield return _factory.Skyscrapper(base1, base2, pos1, pos2, value);
                    }
                }
            }
        }
    }
}
