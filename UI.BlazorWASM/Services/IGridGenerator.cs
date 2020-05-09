using Core.Data;
using System.Threading.Tasks;

namespace UI.BlazorWASM.Services
{
    public interface IGridGenerator
    {
        Task<IGrid> New(string difficulty = "Medium");
    }
}
