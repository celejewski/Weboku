using Core.Data;
using System.Threading.Tasks;

namespace Core.Generators
{
    public interface IGridGenerator : IEmptyGridGenerator
    {
        Task<IGridV2> WithGiven(string difficulty);
    }
}
