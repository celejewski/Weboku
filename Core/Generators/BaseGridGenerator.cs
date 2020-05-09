using Core.Data;

namespace Core.Generators
{
    public class BaseGridGenerator : IEmptyGridGenerator
    {
        public IGrid Empty() => new Grid();
    }
}
