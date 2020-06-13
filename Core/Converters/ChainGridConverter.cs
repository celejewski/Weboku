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
        }

        private IGridConverter GetFirstValidOrDefault(string text)
        {
            return _converters.FirstOrDefault(converter => converter.IsValidText(text));
        }

        public IGridV2 FromText(string text)
        {
            var converter = GetFirstValidOrDefault(text);
            if (converter == null)
            {
                throw new ArgumentException(text);
            }
            return converter.FromText(text);
        }

        public bool IsValidText(string text)
        {
            return GetFirstValidOrDefault(text) != null;
        }

        public string ToText(IGridV2 grid)
        {
            return _converters[0].ToText(grid);
        }
    }
}
