using System.Threading.Tasks;
using Weboku.Core.Data;
using Weboku.Core.Serializers;

namespace Weboku.Application.Managers.GridGenerators
{
    public class PredefinedGridGenerator : IGridGenerator
    {
        public Task<Grid> Make(Difficulty difficulty)
        {
            var hodokuGridSerializer = new HodokuGridSerializer();
            var input = "000280000329000600000030009531000060008001090004870000096105720000000000000000040";
            var grid = hodokuGridSerializer.Deserialize(input);
            return Task.FromResult(grid);
        }
    }
}