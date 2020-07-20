using Core.Data;
using Core.Helpers;
using SmartSolver.SolvingTechniques;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolver.TechniqueFinders
{
    public class FullHouseFinder : BaseTechniqueFinder
    {
        private readonly ISolvingTechniquesFactory _factory;

        public FullHouseFinder(ISolvingTechniquesFactory factory)
        {
            _factory = factory;
        }

        public override IEnumerable<ISolvingTechnique> FindAll(IGrid grid)
        {
            foreach( var indexes in Position.Houses )
            {
                if( indexes.WithValue(grid).Count() == 8 )
                {
                    var value = InputValue.NonEmpty.First(
                        value => indexes.All(index => grid.GetValue(index) != value));
                    var pos = indexes.First(index => !grid.HasValue(index));

                    yield return _factory.FullHouse(pos, value);
                }
            }
        }

        public override string ToString()
        {
            return nameof(FullHouseFinder);
        }
    }
}
