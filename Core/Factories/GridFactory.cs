using Core.Converters;
using Core.Data;
using Core.Generators;

namespace Core.Factories
{
    public static class GridFactory
    {
        private static readonly IGridConverter _converter;

        static GridFactory()
        {
            var generator = new EmptyGridGenerator();
            _converter = new ChainGridConverter(
                new HodokuGridConverter(generator),
                new Base64GridConverter(generator));
        }

        public static IGrid FromText(string text)
        {
            return _converter.FromText(text);
        }
    }
}
