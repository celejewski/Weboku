using Core.Data;

namespace Core.Generators
{
    public class EmptyGridGenerator : IEmptyGridGenerator
    {
        public IGrid Empty() => new Grid();
    }
}
