using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Converters
{
    public class ChainGridConverter : IGridConverter
    {
        private readonly List<IGridConverter> _converters = new List<IGridConverter>();

        public ChainGridConverter(
            HodokuGridConverter hodokuGridConverter,
            Base64GridConverter base64GridConverter)
        {
            _converters.Add(base64GridConverter);
            _converters.Add(hodokuGridConverter);
            _converters.Add(new Base64CandidatesConverter());
        }

        private IGridConverter GetFirstValidOrDefault(string text)
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
