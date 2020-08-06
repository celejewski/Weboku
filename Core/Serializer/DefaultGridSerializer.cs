using Core.Data;
using Core.Exceptions;
using System.Collections.Generic;

namespace Core.Serializer
{
    internal class DefaultGridSerializer : IGridSerializer
    {
        private readonly List<IGridSerializer> _converters = new List<IGridSerializer>();

        public DefaultGridSerializer()
        {
            _converters.Add(new Base64GridSerializer());
            _converters.Add(new HodokuGridSerializer());
            _converters.Add(new Base64CandidatesSerializer());
        }

        private IGridSerializer GetFirstValidOrDefault(string text)
        {
            return _converters.Find(converter => converter.IsValidFormat(text));
        }

        public IGrid Deserialize(string text)
        {
            var converter = GetFirstValidOrDefault(text);
            if( converter == null )
            {
                throw new GridSerializationException($"Exception in {nameof(DefaultGridSerializer)} occured during {nameof(Deserialize)} with value \"{text}\"");
            }
            return converter.Deserialize(text);
        }

        public bool IsValidFormat(string text)
        {
            return GetFirstValidOrDefault(text) != null;
        }

        public string Serialize(IGrid grid)
        {
            return _converters[0].Serialize(grid);
        }
    }
}
