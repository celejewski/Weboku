using Core.Data;
using System.Threading.Tasks;

namespace Core.Generators
{
    public interface INewGivenGenerator
    {
        Task<IGrid> NewGiven(string difficulty);
    }
}
