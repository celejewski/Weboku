using Core.Data;
using System;
using System.Collections.Generic;

namespace Core.Serializer
{
    public class ChainGridSerializer : IGridSerializer
    {
        private readonly List<IGridSerializer> _converters = new List<IGridSerializer>();

        public ChainGridSerializer(
            HodokuGridSerializer hodokuGridConverter,
            Base64GridSerializer base64GridConverter)
        {
            _converters.Add(base64GridConverter);
            _converters.Add(hodokuGridConverter);
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
                throw new ArgumentException(text);
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
