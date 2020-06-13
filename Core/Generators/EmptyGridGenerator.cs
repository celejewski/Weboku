using Core.Data;

namespace Core.Generators
{
    public class EmptyGridGenerator : IEmptyGridGenerator
    {
        public IGridV2 Empty() => new GridV2();
    }
}
