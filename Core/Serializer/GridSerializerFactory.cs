using Core.Generators;

namespace Core.Serializer
{
    public static class GridSerializerFactory
    {
        public static IGridSerializer Make(GridSerializerName name, GridSerializerMode mode = GridSerializerMode.Everything)
        {
            var empty = new EmptyGridGenerator();
            var hodoku = new HodokuGridSerializer(empty);
            var base64 = new Base64GridSerializer(empty);
            var base64WithCandidates = new Base64CandidatesSerializer();
            return (name, mode) switch
            {
                (GridSerializerName.Hodoku, _) => hodoku,
                (GridSerializerName.Base64, GridSerializerMode.Everything) => base64WithCandidates,
                (GridSerializerName.Base64, _) => base64,
                _ => new DefaultGridSerializer(new HodokuGridSerializer(empty), base64),
            };
        }
    }
}
