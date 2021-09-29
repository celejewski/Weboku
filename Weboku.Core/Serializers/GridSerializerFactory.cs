namespace Weboku.Core.Serializers
{
    public static class GridSerializerFactory
    {
        public static IGridSerializer Make(GridSerializerName name, GridSerializerMode mode = GridSerializerMode.Everything)
        {
            var hodoku = new HodokuGridSerializer();
            var base64 = new Base64GridSerializer();
            var base64WithCandidates = new Base64CandidatesSerializer();
            return (name, mode) switch
            {
                (GridSerializerName.Hodoku, _) => hodoku,
                (GridSerializerName.Base64, GridSerializerMode.Everything) => base64WithCandidates,
                (GridSerializerName.Base64, _) => base64,
                _ => new DefaultGridSerializer(),
            };
        }
    }
}