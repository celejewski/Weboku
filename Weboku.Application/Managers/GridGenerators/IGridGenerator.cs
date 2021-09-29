using System.Threading.Tasks;
using Weboku.Core.Data;

namespace Weboku.Application.Managers.GridGenerators
{
    public interface IGridGenerator
    {
        Task<Grid> Make(Difficulty difficulty);
    }
}