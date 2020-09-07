using Core.Data;
using Core.Hints.SolvingTechniques;
using System.Collections.Generic;

namespace Core.Hints.TechniqueFinders
{
    public interface ITechniqueFinder
    {
        public ISolvingTechnique Find(IGrid grid);
        public IEnumerable<ISolvingTechnique> FindAll(IGrid grid);
    }
}
