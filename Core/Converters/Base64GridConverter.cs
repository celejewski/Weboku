using Core.Data;
using Core.Generators;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Numerics;
using System.Text;

namespace Core.Converters
{
    public class Base64GridConverter : IGridConverter
    {
        private readonly IGridConverter _innerConverter;

        public Base64GridConverter(IEmptyGridGenerator emptyGridGenerator)
        {
            _innerConverter = new HodokuGridConverter(emptyGridGenerator);
        }

        public IGrid FromText(string text)
        {
            var bytes = WebEncoders.Base64UrlDecode(text);
            var bigInt = new BigInteger(bytes);
            var parsed = bigInt.ToString().Substring(1);

            if( parsed.Length != 81 )
            {
                throw new ArgumentException();
            }

            return _innerConverter.FromText(parsed);
        }

        public bool IsValidText(string text)
        {
            try
            {
                FromText(text);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string ToText(IGrid grid)
        {
            var sb = new StringBuilder();
            sb.Append("1");
            foreach( var pos in Position.All )
            {
                sb.Append(grid.GetValue(pos));
            }
            var bigInt = BigInteger.Parse(sb.ToString());
            var bytes = bigInt.ToByteArray();
            return WebEncoders.Base64UrlEncode(bytes);
        }
    }
}
