using Core.Data;
using Core.Hints.SolvingTechniques;
using System.Collections.Generic;

namespace Core.Hints.TechniqueFinders
{
    public interface ITechniqueFinder
    {
        public ISolvingTechnique Find(Grid grid);
        public IEnumerable<ISolvingTechnique> FindAll(Grid grid);
    }
}
