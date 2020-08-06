using Core.Data;
using Core.Generators;
using Core.Serializer;

namespace Core.Factories
{
    public static class GridFactory
    {
        private static readonly IGridSerializer _converter;

        static GridFactory()
        {
            var generator = new EmptyGridGenerator();
            _converter = new DefaultGridSerializer(
                new HodokuGridSerializer(generator),
                new Base64GridSerializer(generator));
        }

        public static IGrid FromText(string text)
        {
            return _converter.Deserialize(text);
        }
    }
}
