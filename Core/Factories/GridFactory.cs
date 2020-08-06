using Core.Data;
using Core.Serializer;

namespace Core.Factories
{
    public static class GridFactory
    {
        private static readonly IGridSerializer _converter;

        static GridFactory()
        {
            _converter = new DefaultGridSerializer();
        }

        public static IGrid Deserialize(string text)
        {
            return _converter.Deserialize(text);
        }

        public static IGrid MakeEmpty() => new Grid();
    }
}
