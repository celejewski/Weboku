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
            return _innerConverter.FromText(bigInt.ToString().Substring(1));
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
            for( int x = 0; x < 9; x++ )
            {
                for( int y = 0; y < 9; y++ )
                {
                    sb.Append(grid.Cells[y, x].Input.Value);
                }
            }
            var bigInt = BigInteger.Parse(sb.ToString());
            var bytes = bigInt.ToByteArray();
            return WebEncoders.Base64UrlEncode(bytes);
        }
    }
}
