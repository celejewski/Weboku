﻿using System.Collections.Generic;
using System.Linq;
using Weboku.Core.Data;
using Weboku.Core.Hints.SolvingTechniques;

namespace Weboku.Core.Hints.TechniqueFinders
{
    public class SkyscrapperFinder : TechniqueFinderBase
    {
        public override IEnumerable<ISolvingTechnique> FindAll(Grid grid)
        {
            foreach (var value in Value.NonEmpty)
            {
                foreach (var houses in new[] {Position.Rows, Position.Cols})
                {
                    var housesWithCandidate = houses.Select(house => house.Where(pos => grid.HasCandidate(pos, value)));
                    var housesWithTwoCells = housesWithCandidate.Where(house => house.Count() == 2);

                    var housesPaired = housesWithTwoCells.SelectMany(
                        (house1, i) => housesWithTwoCells.Skip(i + 1).Select(house2 => (house1, house2)));

                    var possibleSkyscrappers = housesPaired
                        .Where(houses =>
                            houses.house1.Any(pos1 => houses.house2.Any(pos2 => pos1.X == pos2.X || pos1.Y == pos2.Y))
                            && houses.house1.Any(pos1 => houses.house2.All(pos2 => !pos1.IsSharingHouseWith(pos2)))
                            && houses.house1.Concat(houses.house2).Select(pos => pos.Block).Distinct().Count() == 4
                        );

                    foreach (var (house1, house2) in possibleSkyscrappers)
                    {
                        var pos1 = house1.First(pos1 => house2.All(pos2 => !pos1.IsSharingHouseWith(pos2)));
                        var pos2 = house2.First(pos2 => house1.All(pos1 => !pos2.IsSharingHouseWith(pos1)));

                        var positionsToRemoveFrom = Position.GetPositionsSeenByAll(pos1, pos2)
                            .Where(pos => grid.HasCandidate(pos, value));

                        var base1 = house1.First(pos => !pos.Equals(pos1));
                        var base2 = house2.First(pos => !pos.Equals(pos2));

                        yield return new Skyscrapper(base1, base2, pos1, pos2, value);
                    }
                }
            }
        }
    }
}